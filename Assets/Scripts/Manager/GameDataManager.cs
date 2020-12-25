using Assets.Scripts.EnumFlags;
using Assets.Scripts.Models;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GAMESTATE
{
    start = 0,
    game,
    over
}
public class GameDataManager : MonoBehaviour
{
    public GAMESTATE gameState;
    public static GameDataManager _instance;

    /// <summary>
    /// 所有的组件 资源加载
    /// </summary>
    private List<Object> gerneralCompentObject;
    private List<Object> lowerCompentObject;
    private List<Object> middleCompentObject;
    private List<Object> seniorCompentObject;

    [HideInInspector]
    public Object startPointObject;
    [HideInInspector]
    public Object endPointObject;

    /// <summary>
    /// 地图组件总数
    /// </summary>
    public int componentTotalNum;

    /// <summary>
    /// 地图信息
    /// </summary>
    [HideInInspector]
    public List<MapData> mapDatas;

    /// <summary>
    /// 所有的地图组件信息
    /// </summary>
    private List<mapComponent> MapCompenentData;


    /// <summary>
    /// 小球能力
    /// </summary>
    private List<ballSkills> BallSkillsData;

    /// <summary>
    /// AI球数据
    /// </summary>
    private List<AIBallData> AIBallDatas;

    /// <summary>
    /// 小球皮肤数据
    /// </summary>
    [HideInInspector]
    public  List<ballSkinTask> BallSkinTaskData;

    private Dictionary<int,Material> ballMaterials;

    /// <summary>
    /// 金币收益数据
    /// </summary>
    private List<goldUpgrade> goldIncomeUpgradeData;


    #region  游戏任务完成情况的数据统计
    /// <summary>
    /// 累计登陆天数
    /// </summary>
    private int iLoginDayNum;

    /// <summary>
    /// 游戏累计复活次数
    /// </summary>
    private int iReviveNum;

    /// <summary>
    /// 游戏进度 0%-100%
    /// </summary>
    private int iGameProgress;

    /// <summary>
    /// 击杀AI的数量
    /// </summary>
    private int iKillAINum;

    /// <summary>
    /// 完成前百分之十的路程且不击杀AI球
    /// </summary>
    private int iNOT_KillAI_InFirst10P;

    

    /// <summary>
    /// 检查游戏进度 - 任务开启时调用即可
    /// </summary>
    public int getGameProgress()
    {
        iGameProgress = PlayerPrefs.GetInt(appSetting.gameProgress_playerprefs,0);
        return iGameProgress;
    }

    /// <summary>
    /// 检查是否完成前百分之十的路程且不击杀AI球
    /// </summary>
    void checkNOT_KillAI_InFirst10P()
    {
        iNOT_KillAI_InFirst10P = PlayerPrefs.GetInt(appSetting.NOT_KillAI_InFirst10P_playerprefs, 0);
    }

    /// <summary>
    /// 检查击杀AI球的数量  默认为0  未完成
    /// </summary>
   public int  getKillAINum()
    {
        iKillAINum = PlayerPrefs.GetInt(appSetting.killAINum_playerprefs,0);
        return iKillAINum;
    }

    /// <summary>
    /// 检查登陆天数
    /// </summary>
    void checkLoginDay()
    {
        System.DateTime date_now = System.DateTime.UtcNow;
        System.DateTime date_lastLogin = System.DateTime.Parse(PlayerPrefs.GetString(appSetting.loginDayTime_playerprefs, System.DateTime.UtcNow.ToString()));

        iLoginDayNum = PlayerPrefs.GetInt(appSetting.loginDayNum_playerprefs, 1);

        System.TimeSpan spanDay = date_now.Subtract(date_lastLogin);

        if (spanDay.Days == 1)//累加1
        {
            iLoginDayNum += 1;
            PlayerPrefs.SetInt(appSetting.loginDayNum_playerprefs, iLoginDayNum);
        }
        else if (spanDay.Days > 1)//重新计数
        {
            iLoginDayNum = 1;
            PlayerPrefs.SetInt(appSetting.loginDayNum_playerprefs, 1);
        }

        PlayerPrefs.SetString(appSetting.loginDayTime_playerprefs, System.DateTime.UtcNow.ToString());
    }

    /// <summary>
    /// 检查复活次数 - 开启任务时调用即可
    /// </summary>
    void checkReviveNum()
    {
        iReviveNum = PlayerPrefs.GetInt(appSetting.reviveNum_playerprefs);
    }

    /// <summary>
    ///  设置任务完成  1 为完成  
    /// </summary>
    void setNOT_KillAI_InFirst10P()
    {
        PlayerPrefs.SetInt(appSetting.NOT_KillAI_InFirst10P_playerprefs, 1);
    }



    #endregion

    /// <summary>
    /// 记录复活次数 +1
    /// </summary>
    public void  setReviveNum()
    {
        PlayerPrefs.SetInt(appSetting.reviveNum_playerprefs, PlayerPrefs.GetInt(appSetting.reviveNum_playerprefs) + 1);
    }

    /// <summary>
    /// 更新游戏进度
    /// </summary>
    /// <param name="_progress"></param>
    void setGameProgress(int _progress)
    {
        iGameProgress = PlayerPrefs.GetInt(appSetting.gameProgress_playerprefs, 0);
        if (_progress > iGameProgress)
            PlayerPrefs.SetInt(appSetting.gameProgress_playerprefs, _progress);


    }

    /// <summary>
    /// 更新击杀AI球的数量
    /// </summary>
    public void setKillAINum()
    {
        PlayerPrefs.SetInt(appSetting.killAINum_playerprefs,PlayerPrefs.GetInt(appSetting.killAINum_playerprefs)+1);
    }

    /// <summary>
    /// 获取金币收益能力
    /// </summary>
    /// <returns></returns>
    public int getGoldMultipleLevel()
    {
        return PlayerPrefs.GetInt(appSetting.GoldMultipleLevel_playerprefs,1);
    }

    /// <summary>
    /// 获取小球power 等级
    /// </summary>
    /// <returns></returns>
    public int getBallPowerLevel()
    {
        return PlayerPrefs.GetInt(appSetting.BallPowerLevel_playerprefs,1);
    }


    private void Awake()
    {
        _instance = this;
        ///任务数据检查
        checkLoginDay();


        init();
    }




    void init()
    {



        getMapCompenentInfo();
        getMapDataInfo();
        getBallSkillsData();
        getGoldIncomeUpgradeData();
        getBallTasksInfo();
        getAIBallData();


    }

    /// <summary>
    /// 获取地图信息
    /// </summary>
    private void getMapDataInfo()
    {
        mapDatas = JsonDataTool.GetListFromJson<MapData>(appSetting.mapDataTableName);
        ///计算地图组件总数
        ///
        for (int i = 0; i < mapDatas.Count; i++)
        {
            componentTotalNum += mapDatas[i].compenentNumb;
        }

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
        return PlayerPrefs.GetInt(appSetting.goldDataName_PlayerPrefs,0) ;
    }


 

    

    /// <summary>
    /// 获取所有的地图组件信息
    /// </summary>
    private void getMapCompenentInfo()
    {
        MapCompenentData = JsonDataTool.GetListFromJson<mapComponent>(appSetting.mapComponentTableName);

        if (MapCompenentData != null)
        {
            gerneralCompentObject = new List<Object>();
            lowerCompentObject = new List<Object>();
            middleCompentObject = new List<Object>();
            seniorCompentObject = new List<Object>();

            for (int i = 0; i < MapCompenentData.Count; i++)
            {
                switch(MapCompenentData[i].componentDifficulty)
                {
                    case (int)enumcomponentDifficultyType.general:
                        gerneralCompentObject.Add(Resources.Load("prefabs/map/" + MapCompenentData[i].componentResourceName));
                        break;
                    case (int)enumcomponentDifficultyType.lower:
                        lowerCompentObject.Add(Resources.Load("prefabs/map/" + MapCompenentData[i].componentResourceName));
                        break;
                    case (int)enumcomponentDifficultyType.middle:
                        middleCompentObject.Add(Resources.Load("prefabs/map/" + MapCompenentData[i].componentResourceName));
                        break;
                    case (int)enumcomponentDifficultyType.senior:
                        seniorCompentObject.Add(Resources.Load("prefabs/map/" + MapCompenentData[i].componentResourceName));
                        break;

                    case (int)enumcomponentDifficultyType.startPoint:
                        startPointObject =  Resources.Load("prefabs/map/" + MapCompenentData[i].componentResourceName);
                        break;

                    case (int)enumcomponentDifficultyType.endPoint:
                        endPointObject = Resources.Load("prefabs/map/" + MapCompenentData[i].componentResourceName);
                        break;
                }
              
            }
        }
    }

    /// <summary>
    /// 随机获取普通组件
    /// </summary>
    /// <returns></returns>
    public  Object getGeneralComponentObject()
    {
        int x = ConvertHelper.getRandomNumber(0, gerneralCompentObject.Count - 1);
        return gerneralCompentObject[x];
    }

    /// <summary>
    /// 获取低级组件
    /// </summary>
    /// <returns></returns>
    public Object getLowerComponentObject()
    {
        return lowerCompentObject[ConvertHelper.getRandomNumber(0, lowerCompentObject.Count-1)];

    }
    /// <summary>
    /// 随机获取中级组件
    /// </summary>
    /// <returns></returns>
    public Object getMiddleComponentObject()
    {
        return middleCompentObject[ConvertHelper.getRandomNumber(0, middleCompentObject.Count-1)];

    }

    /// <summary>
    /// 随机获取高级组件
    /// </summary>
    /// <returns></returns>
    public Object getSeniorComponentObject()
    {
        return seniorCompentObject[ConvertHelper.getRandomNumber(0, seniorCompentObject.Count-1)];

    }

    /// <summary>
    /// 获取小球皮肤任务数据
    /// </summary>
    private void getBallTasksInfo()
    {
        BallSkinTaskData = JsonDataTool.GetListFromJson<ballSkinTask>(appSetting.ballSkinTaskTableName);
        ballMaterials = new Dictionary<int, Material>();

        List<ballTask> ballTasksGameData = new List<ballTask>();

        if (BallSkinTaskData!=null)
        {
            for (int i = 0; i < BallSkinTaskData.Count; i++)
            {
                ballMaterials.Add(BallSkinTaskData[i].id, Resources.Load("mat/" + BallSkinTaskData[i].skinSourceName) as Material);
                if(i == 0)
                {
                    ballTasksGameData.Add(new ballTask
                    {
                        id = BallSkinTaskData[i].id,
                        skinSourceName = BallSkinTaskData[i].skinSourceName,
                        taskInfo = BallSkinTaskData[i].taskInfo,
                        taskProgress = 100,
                        isSelected = 1
                    });
                }
                else
                {
                    ballTasksGameData.Add(new ballTask
                    {
                        id = BallSkinTaskData[i].id,
                        skinSourceName = BallSkinTaskData[i].skinSourceName,
                        taskInfo = BallSkinTaskData[i].taskInfo,
                        taskProgress = 0,
                        isSelected = 0
                    });
                }
            
            }
            ///创建可修改数据表
            ///
            setBallTaskGameData(ballTasksGameData);
        }
    }

    void setBallTaskGameData(List<ballTask> _tasksGameData)
    {
        JsonDataTool.SetJsonFromList<ballTask>(appSetting.ballSkinGameDataTableName, _tasksGameData);
    }

    /// <summary>
    /// 获取并且修改小球任务信息表
    /// </summary>
    public List<ballTask> get_change_BallSkinTaskGameData()
    {
        List<ballTask> data = JsonDataTool.GetListFromJson<ballTask>(appSetting.ballSkinGameDataTableName);
        getGameProgress();
        checkReviveNum();
        getKillAINum();
        checkNOT_KillAI_InFirst10P();
        foreach (var item in data)
        {
           switch(item.id)
            {
                case 1:
                    item.taskProgress = iLoginDayNum / 3;
                    break;
                case 2:
                    item.taskProgress = iLoginDayNum / 5;
                    break;
                case 3:
                    item.taskProgress = iLoginDayNum / 7;
                    break;
                case 4:item.taskProgress = iReviveNum / 10;
                    break;
                case 5:
                    item.taskProgress = iReviveNum / 20;
                    break;
                case 6:
                    item.taskProgress = iGameProgress / 10;
                    break;
                case 7:
                    item.taskProgress = iGameProgress / 30;
                    break;
                case 8:
                    item.taskProgress = iGameProgress / 50;
                    break;
                case 9:
                    item.taskProgress = iGameProgress / 80;
                    break;
                case 10:
                    item.taskProgress = iKillAINum / 10;
                    break;
                case 11:
                    item.taskProgress = iNOT_KillAI_InFirst10P / 1;
                    break;
                case 12:
                    item.taskProgress = iGameProgress / 100;
                    break;

            }
        }

        setBallTaskGameData(data);

        return data;
    }

    /// <summary>
    /// 根据小球的皮肤ID获取相应的材质球
    /// </summary>
    /// <param name="skinId"></param>
    /// <returns></returns>
    public Material getBallSkinForSkinId(int skinId = 0)
    {
        foreach (var item in ballMaterials)
        {
            if(item.Key == skinId)
            {
                return item.Value;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据小球皮肤ID获取小球皮肤对应的任务信息
    /// </summary>
    /// <param name="skinId"></param>
    /// <returns></returns>
    public string getBallSkinTaskForSkinId(int skinId = 0)
    {
        foreach(var item in BallSkinTaskData)
        {
            if (item.id == skinId)
            {
                return item.taskInfo;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取金币收益等级表
    /// </summary>
    private void getGoldIncomeUpgradeData()
    {
        goldIncomeUpgradeData = JsonDataTool.GetListFromJson<goldUpgrade>(appSetting.goldIncomeUpgardeTableName);
    }

    /// <summary>
    /// 根据等级获取数据
    /// </summary>
    /// <param name="_level"></param>
    /// <returns></returns>
    public goldUpgrade getGoldUpgradeForLevel(int _level)
    {
        foreach (var item in goldIncomeUpgradeData)
        {
            if (item.goldLevel == _level)
                return item;
        }
        return null;
    }

    /// <summary>
    /// 获取小球能力等级数据
    /// </summary>
    private void getBallSkillsData()
    {
        BallSkillsData = JsonDataTool.GetListFromJson<ballSkills>(appSetting.ballSkillsTableName);
    }

    /// <summary>
    /// 获取AI小球等级数据
    /// </summary>
    private void getAIBallData()
    {
        AIBallDatas = JsonDataTool.GetListFromJson<AIBallData>(appSetting.AIBallDataTableName);
    }


    /// <summary>
    /// 根据AI球的等级获取AI的数据
    /// </summary>
    /// <param name="_level"></param>
    public AIBallData getAIBallDataForLevel(int _level)
    {
        foreach (var item in AIBallDatas)
        {
            if (item.level == _level)
                return item;
        }
        return null;
    }


    /// <summary>
    /// 根据level 查找对应数据
    /// </summary>
    /// <param name="_level"></param>
    /// <returns></returns>
    public ballSkills getBallSkillForLevel(int _level)
    {
        foreach (var item in BallSkillsData)
        {
            if (item.level == _level)
                return item;
        }
        return null;
    }










}
