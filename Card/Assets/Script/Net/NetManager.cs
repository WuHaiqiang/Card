using Protocol.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 网络模块
/// </summary>
public class NetManager : ManagerBase
{
    public static NetManager Instance;

    private void Start()
    {
        client.Connect();
    }

    private ClientPeer client = new ClientPeer("127.0.0.1", 6666);

    void Update()
    {
        if (client == null)
            return;

        while (client.socketMsgList.Count > 0)
        {
            SocketMsg msg = client.socketMsgList.Dequeue();
            //处理消息
            processSocketMsg(msg);
        }
    }

    #region 处理接收到的服务器发来的消息

    HandlerBase accountHandler = new AccountHandler();
    HandlerBase userHandler = new UserHandler();
    HandlerBase matchHandler = new MatchHandler();

    /// <summary>
    /// 接收网络的消息
    /// </summary>
    private void processSocketMsg(SocketMsg msg)
    {
        switch (msg.OpCode)
        {
            case OpCode.ACCOUNT:
                accountHandler.OnReceive(msg.SubCode, msg.Value);
                break;
            case OpCode.USER:
                userHandler.OnReceive(msg.SubCode, msg.Value);
                break;
            case OpCode.MATCH:
                matchHandler.OnReceive(msg.SubCode, msg.Value);
                break;
            default:
                break;
        }
    }

    #endregion

    #region 处理客户端内部 给服务器发消息的 事件

    private void Awake()
    {
        Instance = this;

        Add(0, this);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case 0:
                client.Send(message as SocketMsg);
                break;
            default:
                break;
        }
    }

    #endregion
}

