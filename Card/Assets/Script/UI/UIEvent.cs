using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 存储所有的UI事件码
/// </summary>
public class UIEvent
{
    public const int START_PANEL_ACTIVE = 0;//设置开始面板的显示
    public const int REGIST_PANEL_ACTIVE = 1;//设置注册面板的显示

    public const int REFRESH_INFO_PANEL = 2;//刷新信息面板 参数： 服务器定的
    public const int SHOW_ENTER_BUTTON = 3;//显示进入房间按钮
    public const int CREATE_PANEL_ACTIVE = 4;//设置创建面板的显示

    public const int SET_TABLE_CARDS = 5;//设置底牌
    public const int SET_LEFT_PLAYER_DATA = 6;//设置左边的角色的数据
    public const int SET_RIGHT_PLAYER_DATA = 13;//设置右边的角色的数据
    public const int SET_MY_PLAYER_DATA = 16;//设置自身的角色数据

    public const int PLAYER_READY = 7;//角色准备
    public const int PLAYER_ENTER = 8;//角色进入
    public const int PLAYER_LEAVE = 9;//角色离开
    public const int PLAYER_CHAT = 10;//角色聊天
    public const int PLAYER_CHANGE_IDENTITY = 11;//角色更改身份
    public const int PLAYER_HIDE_STATE = 12;//开始游戏 角色隐藏状态面板

    public const int SHOW_GRAB_BUTTON = 14;//开始抢地主 显示抢地主按钮
    public const int SHOW_DEAL_BUTTON = 15;//开始出牌 显示出牌按钮

    //...

    public const int PROMPT_MSG = int.MaxValue;
}
