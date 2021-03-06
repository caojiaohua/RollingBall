﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ballTask 
{
    /// <summary>
    /// id
    /// </summary>
    public int id;

    /// <summary>
    /// 资源名称
    /// </summary>
    public string skinSourceName;

    /// <summary>
    /// 任务信息
    /// </summary>
    public string taskInfo;

    /// <summary>
    /// 任务进度 默认为0  
    /// </summary>
    public double taskProgress;

    /// <summary>
    /// 是否被选择  默认为0 - 没被选择  1- 被选择
    /// </summary>
    public int isSelected;


}
