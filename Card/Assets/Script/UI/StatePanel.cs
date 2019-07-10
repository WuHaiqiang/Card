using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protocol.Dto;

public class StatePanel : UIBase
 {
    /// <summary>
    /// 角色的数据
    /// </summary>
    protected UserDto userDto;

    protected Image imgIdentity;
    protected Text txtReady;
    protected Image imgChat;
    protected Text txtChat;

	protected virtual void Start ()
    {
        imgIdentity = transform.Find("imgIdentity").GetComponent<Image>();
        txtReady = transform.Find("txtReady").GetComponent<Text>();
        imgChat = transform.Find("imgChat").GetComponent<Image>();
        txtChat = imgChat.transform.Find("txtChat").GetComponent<Text>();

        //默认状态
        txtReady.gameObject.SetActive(false);
        imgChat.gameObject.SetActive(false);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.PLAYER_READY:
                {
                    int userId = (int)message;
                    //如果是自身角色 就显示
                    if (userDto.Id == userId)
                        readyState();
                    break;
                }
            case UIEvent.PLAYER_HIDE_STATE:
                {
                    txtReady.gameObject.SetActive(false);
                }
                break;
            case UIEvent.PLAYER_LEAVE:
                {
                    int userId = (int)message;
                    if (userDto.Id == userId)
                        setPanelActive(false);
                    break;
                }
            case UIEvent.PLAYER_ENTER:
                {
                    int userId = (int)message;
                    if (userDto.Id == userId)
                        setPanelActive(true);
                    break;
                }
            default:
                break;
        }
    }

    protected virtual void readyState()
    {
        txtReady.gameObject.SetActive(true);
    }

    /// <summary>
    /// 设置身份
    ///     0 就是农民 1 是地主
    /// </summary>
    protected void setIdentity(int identity)
    {
        string identityStr = identity == 0 ? "Farmer" : "Landlord";
        imgIdentity.sprite = Resources.Load<Sprite>("Identity/" + identityStr);
    }

    /// <summary>
    /// 显示时间
    /// </summary>
    protected int showTime = 2;
    /// <summary>
    /// 计时器
    /// </summary>
    protected float timer = 0f;
    /// <summary>
    /// 是否显示
    /// </summary>
    protected bool isShow = false;

    protected virtual void Update()
    {
        if (isShow)
        {
            timer += Time.deltaTime;
            if(timer >= showTime)
            {
                setChatActive(false);
                timer = 0;
                isShow = false;
            }
        }
    }

    protected void setChatActive(bool active)
    {
        imgChat.gameObject.SetActive(active);
    }

    /// <summary>
    /// 外界调用的 显示聊天
    /// </summary>
    /// <param name="text"></param>
    protected void showChat(string text)
    {
        //设置文字
        txtChat.text = text;
        //显示动画
        setChatActive(true);
        isShow = true;
    }

}
