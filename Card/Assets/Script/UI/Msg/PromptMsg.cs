using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PromptMsg
{
    public string Text;
    public Color Color;

    public PromptMsg()
    {

    }

    public PromptMsg(string text, Color color)
    {
        Text = text;
        Color = color;
    }

    /// <summary>
    /// 避免了频繁应用对象
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public void Change(string text, Color color)
    {
        Text = text;
        Color = color;
    }
}
