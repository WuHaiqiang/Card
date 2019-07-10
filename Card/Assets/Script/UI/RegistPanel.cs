using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistPanel : UIBase
 {

    private Button btnRegist;
    private Button btnClose;
    private InputField inputAccount;
    private InputField inputPassword;
    private InputField inputRepeat;

    private PromptMsg promptMsg;
    private SocketMsg socketMsg;

    void Awake()
    {
        Bind(UIEvent.REGIST_PANEL_ACTIVE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REGIST_PANEL_ACTIVE:
                setPanelActive((bool)message);
                break;

            default:
                break;
        }
    }

    void Start()
    {
        btnRegist = transform.Find("btnRegist").GetComponent<Button>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        inputAccount = transform.Find("inputAccount").GetComponent<InputField>();
        inputPassword = transform.Find("inputPassword").GetComponent<InputField>();
        inputRepeat = transform.Find("inputRepeat").GetComponent<InputField>();

        btnRegist.onClick.AddListener(OnbtnRegist);
        btnClose.onClick.AddListener(OnbtnClose);

        promptMsg = new PromptMsg();
        socketMsg = new SocketMsg();

        setPanelActive(false);
    }

    //AccountDto dto = new AccountDto();
    //SocketMsg socketMsg = new SocketMsg();

    private void OnbtnRegist()
    {
        if (string.IsNullOrEmpty(inputAccount.text))
        {
            promptMsg.Change("注册的用户名不能为空", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        if (string.IsNullOrEmpty(inputPassword.text) || inputPassword.text.Length < 4 || inputPassword.text.Length > 16)
        {
            promptMsg.Change("注册的密码不合法", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        if (string.IsNullOrEmpty(inputRepeat.text) || inputRepeat.text != inputPassword.text)
        {
            promptMsg.Change("请确保两次输入的密码一致", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }

        AccountDto dto = new AccountDto(inputAccount.text, inputPassword.text);
        socketMsg.Change(OpCode.ACCOUNT, AccountCode.REGIST_CREQ, dto);
        //dto.Account = inputAccount.text;
        //dto.Password = inputPassword.text;
        //socketMsg.OpCode = OpCode.ACCOUNT;
        //socketMsg.SubCode = AccountCode.REGIST_CREQ;
        //socketMsg.Value = dto;

        Dispatch(AreaCode.NET, 0, socketMsg);
    }

    private void OnbtnClose()
    {
        setPanelActive(false);
    }
}
