using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager _instance;
    /// <summary>
    /// 所有的材质球信息
    /// </summary>
    private List<MAT> allMatInfo;
    /// <summary>
    /// 所有的材质球模型
    /// </summary>
    private List<Material> allMaterials;

    /// <summary>
    /// 关卡信息
    /// </summary>
    private List<LevelData> allLevelData;

    /// <summary>
    /// 所有的地图组件信息
    /// </summary>
    private List<mapComponent> allMapCompenentData;
    /// <summary>
    /// 所有的装饰物数据
    /// </summary>
    private List<Ornament> allOrnamentData;

    /// <summary>
    /// 所有的装饰物资源加载
    /// </summary>
    private List<Object> allOrnamentObject;
    /// <summary>
    /// 所有的道具信息
    /// </summary>
    private List<prop> allPropData;

    /// <summary>
    /// 所有的组件 资源加载
    /// </summary>
    public List<Object> allCompentObject;

    /// <summary>
    /// 所有的道具 资源加载
    /// </summary>
    private List<Object> allPropObject;

  
  

    
    private void Awake()
    {
        _instance = this;
        init();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }



    void init()
    {
        #region  初始化金币 钻石余额
        if (!PlayerPrefs.HasKey(appSetting.goldDataName_PlayerPrefs))
        {
            PlayerPrefs.SetInt(appSetting.goldDataName_PlayerPrefs,appSetting.initGoldNum);
        }
        if (!PlayerPrefs.HasKey(appSetting.diamondDataName_PlayerPrefs))
        {
            PlayerPrefs.SetInt(appSetting.diamondDataName_PlayerPrefs, appSetting.initDiamondNum);
        }
        #endregion

        getMatInfo();
        getMapCompenentInfo();
        getLevelDataInfo();

        ///初始化当前关卡进度
        if (!PlayerPrefs.HasKey("curGameLevel"))
            PlayerPrefs.SetInt("curGameLevel", 1);

    }

    public void getLevelDataInfo()
    {
        allLevelData = JsonDataTool.GetListFromJson<LevelData>(appSetting.levelDataTableName);

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
    /// 设置钻石数量
    /// </summary>
    /// <param name="_udateNum"></param>
    public void setDimondNum(int _udateNum)
    {
        PlayerPrefs.SetInt(appSetting.diamondDataName_PlayerPrefs, getDimondNum() + _udateNum);

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
    /// 获取钻石数量
    /// </summary>
    /// <returns></returns>
    public int getDimondNum()
    {
        return PlayerPrefs.GetInt(appSetting.diamondDataName_PlayerPrefs);
        
    }

    /// <summary>
    /// 获取全部材质球信息
    /// </summary>
    private void getMatInfo()
    {
        allMatInfo = JsonDataTool.GetListFromJson<MAT>(appSetting.MATTableName);

        if(allMatInfo != null)
        {
            allMaterials = new List<Material>();
            for (int i = 0;i<allMatInfo.Count;i++ )
            {
                allMaterials.Add(Resources.Load(allMatInfo[i].matName) as Material);
            }
        }
    }

    /// <summary>
    /// 获取所有的地图组件信息
    /// </summary>
    private void getMapCompenentInfo()
    {
        allMapCompenentData = JsonDataTool.GetListFromJson<mapComponent>(appSetting.mapComponentTableName);

        if (allMapCompenentData != null)
        {
            allCompentObject = new List<Object>();

            for (int i = 0; i < allMapCompenentData.Count; i++)
            {
                allCompentObject.Add(Resources.Load("prefabs/map/"+allMapCompenentData[i].componentResourceName));
            }
        }
    }

    /// <summary>
    /// 获取装饰物数据
    /// </summary>
    private void getAllOrnamentInfo()
    {
        allOrnamentData = JsonDataTool.GetListFromJson<Ornament>(appSetting.ornamentTableName);

        if (allOrnamentData != null)
        {
            allOrnamentObject = new List<Object>();

            for (int i = 0; i < allOrnamentData.Count; i++)
            {
                allOrnamentObject.Add(Resources.Load(allOrnamentData[i].resourceName));
            }
        }
    }

    /// <summary>
    /// 根据ID 获取装饰物信息
    /// </summary>
    /// <returns></returns>
    public Object getOrnamentForID(int id)
    {
        
        return null;
    }

    /// <summary>
    /// 根据ID获取道具信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Object getPropForID(int id)
    {
        return null;
    }

    /// <summary>
    /// 根据ID获取组件Object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Object getMapCompomentForId(int id)
    {
        return null;
    }

    /// <summary>
    /// 获取当前关卡进度
    /// </summary>
    /// <returns></returns>
    public int getCurGameLevel()
    {
        return PlayerPrefs.GetInt("curGameLevel");
    }

    /// <summary>
    /// 设置当前关卡进度
    /// </summary>
    public void setCurGameLevel()
    {
        PlayerPrefs.SetInt("curGameLevel",getCurGameLevel()+1);
    }

    public LevelData getMapInfoForID(int _levelId)
    {
        foreach (LevelData v in allLevelData)
            if (v.levelID == _levelId)
                return v;
        return null;
    }



}
