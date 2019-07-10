using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : HandlerBase
{
    public override void OnReceive(int subCode, object value)
    {
        switch (subCode)
        {
            case MatchCode.ENTER_SRES:
                enterResponse(value as MatchRoomDto);
                break;
            case MatchCode.ENTER_BRO:
                enterBro(value as UserDto);
                break;
            case MatchCode.LEAVE_BRO:
                leaveBro((int)value);
                break;
            case MatchCode.READY_BRO:
                readyBro((int)value);
                break;
            case MatchCode.START_BRO:
                startBro();
                break;
            default:
                break;
        }
    }

    PromptMsg promptMsg = new PromptMsg();

    /// <summary>
    /// 开始游戏的广播处理
    /// </summary>
    private  void startBro()
    {
        promptMsg.Change("所有玩家准备开始游戏", Color.blue);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        //开始游戏 隐藏状态面板的准备文字
        Dispatch(AreaCode.UI, UIEvent.PLAYER_HIDE_STATE, null);
    }

    /// <summary>
    /// 准备的广播处理
    /// </summary>
    /// <param name="readyUserId"></param>
    private void readyBro(int readyUserId)
    {
        //保存数据
        Models.GameModel.MatchRoomDto.Ready(readyUserId);
        //显示为玩家状态面板的准备文字
        Dispatch(AreaCode.UI, UIEvent.PLAYER_READY, readyUserId);
    }

    /// <summary>
    /// 离开的广播处理
    /// </summary>
    /// <param name="leaveUserId"></param>
    private void leaveBro(int leaveUserId)
    {
        //发消息 隐藏玩家的状态面板所有游戏物体
        Dispatch(AreaCode.UI, UIEvent.PLAYER_LEAVE, leaveUserId);

        resetPosition();

        //保存数据
        Models.GameModel.MatchRoomDto.Leave(leaveUserId);
    }

    /// <summary>
    /// 自身进入服务器的响应
    /// </summary>
    /// <param name="room"></param>
    private void enterResponse(MatchRoomDto room)
    {
        //存储本地
        GameModel gModel = Models.GameModel;
        MatchRoomDto matchRoom = gModel.MatchRoomDto;
        int myUserId = gModel.UserDto.Id;
        matchRoom = room;
        //重置一下玩家的位置信息
        gModel.MatchRoomDto.ResetPosition(myUserId);

        //显示现在房间内的玩家
        if(matchRoom.LeftId != -1)
        {
            UserDto leftUserDto = matchRoom.UIdUserDict[matchRoom.LeftId];
            Dispatch(AreaCode.UI, UIEvent.SET_LEFT_PLAYER_DATA, leftUserDto);
        }
        if (matchRoom.RightId != -1)
        {
            UserDto rightUserDto = matchRoom.UIdUserDict[matchRoom.RightId];
            Dispatch(AreaCode.UI, UIEvent.SET_RIGHT_PLAYER_DATA, rightUserDto);
        }

        //自身的角色是肯定在的 可以直接的来更新自身的数据
        UserDto myUserDto = matchRoom.UIdUserDict[myUserId];

        //显示进入房间的按钮
        Dispatch(AreaCode.UI, UIEvent.SHOW_ENTER_BUTTON, null);
    }

    /// <summary>
    /// 他人进入的广播处理
    /// </summary>
    /// <param name="newUser"></param>
    private void enterBro(UserDto newUser)
    {
        //发消息 显示玩家的状态面板所有游戏物体
        Dispatch(AreaCode.UI, UIEvent.PLAYER_ENTER, newUser.Id);

        //更新房间数量
        Models.GameModel.MatchRoomDto.Add(newUser);

        resetPosition();

        //给用户一个提示
        promptMsg.Change("有新玩家（" + newUser.Name + ")进入", Color.green);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
    }

    /// <summary>
    /// 重置位置
    ///     更新左右玩家显示
    /// </summary>
    private void resetPosition()
    {
        GameModel gModel = Models.GameModel;
        MatchRoomDto matchRoom = gModel.MatchRoomDto;

        //重置一下玩家的位置
        matchRoom.ResetPosition(gModel.UserDto.Id);

        //再次发送现在房间内的玩家
        if (matchRoom.LeftId != -1)
        {
            UserDto leftUserDto = matchRoom.UIdUserDict[matchRoom.LeftId];
            Dispatch(AreaCode.UI, UIEvent.SET_LEFT_PLAYER_DATA, leftUserDto);
        }
        if (matchRoom.RightId != -1)
        {
            UserDto rightUserDto = matchRoom.UIdUserDict[matchRoom.RightId];
            Dispatch(AreaCode.UI, UIEvent.SET_RIGHT_PLAYER_DATA, rightUserDto);
        }
    }

}
