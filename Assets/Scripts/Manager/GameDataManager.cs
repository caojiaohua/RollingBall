using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager _instance;

    /// <summary>
    /// 所有的组件 资源加载
    /// </summary>
    private Dictionary<int, Object> allCompentObject;

    /// <summary>
    /// 所有的材质球模型
    /// </summary>
    private List<Material> allMaterials;

    /// <summary>
    /// 关卡信息
    /// </summary>
    private List<MapData> mapDatas;

    /// <summary>
    /// 所有的地图组件信息
    /// </summary>
    private List<mapComponent> allMapCompenentData;





  
  

    
    private void Awake()
    {
        _instance = this;
        init();
    }




    void init()
    {
        #region  初始化金币余额
        if (!PlayerPrefs.HasKey(appSetting.goldDataName_PlayerPrefs))
        {
            PlayerPrefs.SetInt(appSetting.goldDataName_PlayerPrefs,appSetting.initGoldNum);
        }
    
        #endregion


        getMapCompenentInfo();
        getMapDataInfo();

        ///初始化当前关卡进度
        if (!PlayerPrefs.HasKey("curGameLevel"))
            PlayerPrefs.SetInt("curGameLevel", 1);

    }

    /// <summary>
    /// 获取地图信息
    /// </summary>
    public void getMapDataInfo()
    {
        mapDatas = JsonDataTool.GetListFromJson<MapData>(appSetting.mapDataTableName);

    }

    /// <summary>
    /// 设置金币数量
    /// </summary>
    /// <param name="_udateNum"></param>
    public void setGoldNum(int _udateNum)
    {
        PlayerPrefs.SetInt(appSetting.goldDataName_PlayerPrefs,getGoldNum()+_udateNum);
    }

 

    /// <summary>
    /// 获取金币数量
    /// </summary>
    /// <returns></returns>
    public int getGoldNum()
    {
        return PlayerPrefs.GetInt(appSetting.goldDataName_PlayerPrefs) ;
    }


 

    

    /// <summary>
    /// 获取所有的地图组件信息
    /// </summary>
    private void getMapCompenentInfo()
    {
        allMapCompenentData = JsonDataTool.GetListFromJson<mapComponent>(appSetting.mapComponentTableName);

        if (allMapCompenentData != null)
        {
            allCompentObject = new Dictionary<int, Object>();

            for (int i = 0; i < allMapCompenentData.Count; i++)
            {
                allCompentObject.Add(allMapCompenentData[i].mapComponentType, Resources.Load("prefabs/map/"+allMapCompenentData[i].componentResourceName));
            }
        }
    }













}
