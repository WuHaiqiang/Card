using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : ManagerBase
{
    public static SceneMgr Instance = null;

    void Awake()
    {
        Instance = this;

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        Add(SceneEvent.LOAD_SCENE, this);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case SceneEvent.LOAD_SCENE:
                LoadSceneMsg msg = message as LoadSceneMsg;
                loadScene(msg);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 临时变量
    /// </summary>
    private Action OnSceneLoaded = null;

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneBuideIndex"></param>
    private void loadScene(LoadSceneMsg msg)
    {
        if(msg.SceneBuildIndex != -1)
            SceneManager.LoadScene(msg.SceneBuildIndex);
        if (msg.SceneBuildName != null)
            SceneManager.LoadScene(msg.SceneBuildName);
        if (msg.OnSceneLoad != null)
            OnSceneLoaded = msg.OnSceneLoad;
    }

    /// <summary>
    /// 当场景加载完成的时候调用
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (OnSceneLoaded != null)
            OnSceneLoaded();
    }
}
