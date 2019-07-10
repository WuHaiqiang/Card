using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LoadSceneMsg
{
    public int SceneBuildIndex;
    public string SceneBuildName;
    public Action OnSceneLoad;

    public LoadSceneMsg()
    {
        this.SceneBuildIndex = -1;
        this.SceneBuildName = null;
        this.OnSceneLoad = null;
    }

    public LoadSceneMsg(string name, Action onSceneLoad)
    {
        this.SceneBuildIndex = -1;
        this.SceneBuildName = name;
        this.OnSceneLoad = onSceneLoad;
    }

    public LoadSceneMsg(int index, Action onSceneLoad)
    {
        this.SceneBuildIndex = index;
        this.SceneBuildName = null;
        this.OnSceneLoad = onSceneLoad;
    }
}

