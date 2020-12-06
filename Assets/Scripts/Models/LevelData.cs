using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 地图数据类
/// </summary>
[Serializable]
public class LevelData 
{
    /// <summary>
    /// 关卡ID
    /// </summary>
    public int levelID;
    /// <summary>
    /// 地图数据
    /// </summary>
    public string mapValue;
    /// <summary>
    /// 关卡描述 
    /// </summary>
    public string mapDiscription;
    /// <summary>
    /// 关卡钻石数据
    /// </summary>
    public int mapDiamondValue;
    /// <summary>
    /// 关卡金币数据
    /// </summary>
    public int mapGoldValue;
    /// <summary>
    /// 关卡时长
    /// </summary>
    public int levelTimeCount;

    /// <summary>
    /// 通关奖励倍数
    /// </summary>
    public float targetRewardMultiple;

    /// <summary>
    /// 通关奖励数量
    /// </summary>
    public int targetRewardNum;

    /// <summary>
    /// 通关奖励类型  详见 enumRewardType;
    /// </summary>
    public int targetRewardType;
    /// <summary>
    /// 关卡类型  详见  enumLevelType
    /// </summary>
    public int levelType;

    /// <summary>
    /// 关卡材质颜色
    /// </summary>
    public int matType;

    /// <summary>
    /// 天空颜色
    /// </summary>
    public int skyType;

}
