using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 地图数据类
/// </summary>
[Serializable]
public class MapData
{
    /// <summary>
    /// 关卡总长度的百分比
    /// </summary>
    public string levelRating;
    /// <summary>
    /// 每一段的组件个数
    /// </summary>
    public int compenentNumb;
    /// <summary>
    /// 普通组件百分比gernaralComponentRating
    /// </summary>
    public int  gernaralComponentRating;
    /// <summary>
    /// 低级组件百分比
    /// </summary>
    public int lowerCompnenetRating;
    /// <summary>
    /// 中级组件占比
    /// </summary>
    public int middleComponentRating;
    /// <summary>
    /// 高级组件占比
    /// </summary>
    public int seniorComponentRating;

    /// <summary>
    /// AI球等级
    /// </summary>
    public string AILevel;

    /// <summary>
    /// AI球生成概率
    /// </summary>
    public string AIRating;

}
