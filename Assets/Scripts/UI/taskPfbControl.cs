using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskPfbControl : MonoBehaviour
{
    /// <summary>
    /// 球 材质球展示
    /// </summary>
    public Material ball;

    /// <summary>
    /// 任务 文字
    /// </summary>
    public Text tex_Task;
    /// <summary>
    /// 任务进度 文字
    /// </summary>
    public Text tex_Task_Progress;

    /// <summary>
    /// 任务进度条
    /// </summary>
    public Image img_Task_Progress;

    public void setTaskPfbInfo(int skinId = 0)
    {
        ball = GameDataManager._instance.getBallSkinForSkinId(skinId);
        tex_Task.text = GameDataManager._instance.getBallSkinTaskForSkinId(skinId);

        ///任务进度
        ///

    }
}
