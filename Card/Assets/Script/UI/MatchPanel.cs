using Protocol.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : UIBase
{
    void Awake()
    {
        Bind(UIEvent.SHOW_ENTER_BUTTON);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SHOW_ENTER_BUTTON:
                btnEnter.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private Button btnMatch;
    private Image imgBg;
    private Text txtDes;
    private Button btnCancel;
    private Button btnEnter;

    private SocketMsg socketMsg;

    void Start()
    {
        btnMatch = transform.Find("btnMatch").GetComponent<Button>();
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        txtDes = transform.Find("txtDes").GetComponent<Text>();
        btnCancel = transform.Find("btnCancel").GetComponent<Button>();
        btnEnter = transform.Find("btnEnter").GetComponent<Button>();

        btnMatch.onClick.AddListener(matchClick);
        btnCancel.onClick.AddListener(cancelClick);
        btnEnter.onClick.AddListener(enterClick);

        socketMsg = new SocketMsg();

        //默认状态
        objectActive(false);
        btnEnter.gameObject.SetActive(false);
    }

    void Update()
    {
        if (txtDes.gameObject.activeInHierarchy == false)
            return;

        timer += Time.deltaTime;
        if(timer > intervalTime)
        {
            doAnimation();
            timer = 0;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        btnMatch.onClick.RemoveAllListeners();
        btnCancel.onClick.RemoveAllListeners();
        btnEnter.onClick.RemoveAllListeners();
    }

    private void enterClick()
    {
        Dispatch(AreaCode.SCENE, SceneEvent.LOAD_SCENE, 2);
    }

    private void matchClick()
    {
        //向服务器发起开始匹配请求
        socketMsg.Change(OpCode.MATCH, MatchCode.ENTER_CREQ, null);
        Dispatch(AreaCode.NET, 0, socketMsg);

        objectActive(true);
    }

    private void cancelClick()
    {
        //向服务器发起离开匹配请求
        socketMsg.Change(OpCode.MATCH, MatchCode.LEAVE_CREQ, null);
        Dispatch(AreaCode.NET, 0, socketMsg);

        objectActive(false);
    }

    /// <summary>
    /// 控制点击匹配按钮之后需要显示的物体
    /// </summary>
    /// <param name="active"></param>
    private void objectActive(bool active)
    {
        imgBg.gameObject.SetActive(active);
        txtDes.gameObject.SetActive(active);
        btnCancel.gameObject.SetActive(active);
    }

    private string defaultText = "正在寻找房间";
    private int dotCount = 0;
    private float intervalTime = 1f;
    private float timer = 0;

    /// <summary>
    /// 做动画
    /// </summary>
    private void doAnimation()
    {
        txtDes.text = defaultText;
        dotCount++;
        if (dotCount > 3)
        {
            dotCount = 1;
        }

        for (int i = 0; i < dotCount; i++)
        {
            txtDes.text += ".";
        }
    }
}
