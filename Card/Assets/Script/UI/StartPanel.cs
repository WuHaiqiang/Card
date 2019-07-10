using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : UIBase
 {
    private Button btnLogin;
    private Button btnClose;
    private InputField inputAccount;
    private InputField inputPassword;

    private PromptMsg promptMsg;
    private SocketMsg socketMsg;

    void Awake()
    {
        Bind(UIEvent.START_PANEL_ACTIVE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.START_PANEL_ACTIVE:
                setPanelActive((bool)message);
                break;

            default:
                break;
        }
    }

    void Start () {
        btnLogin = transform.Find("btnLogin").GetComponent<Button>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        inputAccount = transform.Find("inputAccount").GetComponent<InputField>();
        inputPassword = transform.Find("inputPassword").GetComponent<InputField>();

        btnLogin.onClick.AddListener(OnbtnLogin);
        btnClose.onClick.AddListener(OnbtnClose);

        promptMsg = new PromptMsg();
        socketMsg = new SocketMsg();

        setPanelActive(false);
    }
	
	private void OnbtnLogin()
    {
        if (string.IsNullOrEmpty(inputAccount.text))
        {
            promptMsg.Change("登录的用户名不能为空", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }

        if (string.IsNullOrEmpty(inputPassword.text) || inputPassword.text.Length < 4 || inputPassword.text.Length > 16)
        {
            promptMsg.Change("登录的密码不合法", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }

        AccountDto dto = new AccountDto(inputAccount.text, inputPassword.text);
        //SocketMsg socketMsg = new SocketMsg(OpCode.ACCOUNT, AccountCode.LOGIN, dto);
        socketMsg.Change(OpCode.ACCOUNT, AccountCode.LOGIN, dto);
        Dispatch(AreaCode.NET, 0, socketMsg);
    }

    private void OnbtnClose()
    {
        setPanelActive(false);
    }
}
