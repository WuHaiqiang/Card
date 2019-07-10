using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStatePanel : StatePanel
 {

    private void Awake()
    {
        Bind(UIEvent.SET_RIGHT_PLAYER_DATA);//,
            //UIEvent.PLAYER_READY);
    }

    public override void Execute(int eventCode, object message)
    {
        base.Execute(eventCode, message);

        switch (eventCode)
        {
            case UIEvent.SET_RIGHT_PLAYER_DATA:
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
