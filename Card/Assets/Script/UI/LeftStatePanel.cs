using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftStatePanel : StatePanel
 {

	private void Awake()
    {
        Bind(UIEvent.SET_LEFT_PLAYER_DATA,
            UIEvent.PLAYER_READY);
    }

    public override void Execute(int eventCode, object message)
    {
        base.Execute(eventCode, message);

        switch (eventCode)
        {
            case UIEvent.SET_LEFT_PLAYER_DATA:
                this.userDto = message as UserDto;
                break;
            default:
                break;
        }
    }

    protected override void Start()
    {
        base.Start();

        setPanelActive(false);
    }
}
