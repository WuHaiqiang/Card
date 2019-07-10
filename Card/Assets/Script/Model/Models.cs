using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    /// <summary>
    /// 游戏数据
    /// </summary>
    public static GameModel GameModel;

    static Models()
    {
        GameModel = new GameModel();
    }

}
