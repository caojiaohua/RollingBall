using Assets.Scripts.EnumFlags;
using Assets.Scripts.Models;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameDataManager :MonoBehaviour
{

    private gamedata gamedatas;
    public static GameDataManager _instance;
    void  Awake()
    {
        _instance = this;

        gamedatas = new gamedata();
        //Debug.Log(gameObject.name);
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;

        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);
        ///任务数据检查
        checkLoginDay();

        init();
    }


    

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
    private  List<ballSkinTask> BallSkinTaskData;

    private static Dictionary<int,Material> ballMaterials;

    /// <summary>
    /// 金币收益数据
    /// </summary>
    private List<goldUpgrade> goldIncomeUpgradeData;


    /// <summary>
    /// 获取全局游戏进度 
    /// </summary>
    public static  float getGameProgress()
    {
        return PlayerPrefs.GetFloat(appSetting.gameProgress_playerprefs);
        
    }

    /// <summary>
    /// 获取是否完成前百分之十的路程且不击杀AI球
    /// </summary>
    public static  int getNOT_KillAI_InFirst10P()
    {
        return PlayerPrefs.GetInt(appSetting.NOT_KillAI_InFirst10P_playerprefs, 0);
    }

    /// <summary>
    /// 获取历史总击杀AI球的数量  默认为0  未完成
    /// </summary>
   public static int  getKillAINum_total()
    {
        return  PlayerPrefs.GetInt(appSetting.killAINum_playerprefs,0);
         
    }

    /// <summary>
    /// 获取地图总数
    /// </summary>
    /// <returns></returns>
    public static int getMapComponentNum()
    {
        List<MapData> mapDatas = getMapDataInfo();
        int num = 0;
        foreach (var item in mapDatas)
        {
            num += item.compenentNumb;
        }

        return num;
    }
    /// <summary>
    /// 检查登陆天数
    /// </summary>
    private void checkLoginDay()
    {
        System.DateTime date_now = System.DateTime.UtcNow;
        System.DateTime date_lastLogin = System.DateTime.Parse(PlayerPrefs.GetString(appSetting.loginDayTime_playerprefs, System.DateTime.UtcNow.ToString()));


        System.TimeSpan spanDay = date_now.Subtract(date_lastLogin);

        if (spanDay.Days == 1)//累加1
        {
            gamedatas.iLoginDayNum += 1;
            
        }
        else if (spanDay.Days > 1)//重新计数
        {
            gamedatas.iLoginDayNum = 1;
        }

        PlayerPrefs.SetString(appSetting.loginDayTime_playerprefs, System.DateTime.UtcNow.ToString());
    }


    /// <summary>
    /// 获取游戏登陆天数
    /// </summary>
    /// <returns></returns>
    public static int getLoginDayNum()
    {
        return PlayerPrefs.GetInt(appSetting.loginDayNum_playerprefs,1);
    }

    /// <summary>
    /// 检查复活次数 - 开启任务时调用即可
    /// </summary>
    public static int getReviveNum()
    {
        return  PlayerPrefs.GetInt(appSetting.reviveNum_playerprefs);
    }


    /// <summary>
    /// 获取当局游戏进度
    /// </summary>
    /// <returns></returns>
    public static float getCurGameProgress()
    {
        return PlayerPrefs.GetFloat(appSetting.curGameProgress_playerprefs);
    }

    /// <summary>
    /// 获取金币收益能力 等级
    /// </summary>
    /// <returns></returns>
    public static int getGoldMultipleLevel()
    {
        return PlayerPrefs.GetInt(appSetting.GoldMultipleLevel_playerprefs,1);
    }

    /// <summary>
    /// 获取小球power 等级
    /// </summary>
    /// <returns></returns>
    public static int getBallPowerLevel()
    {
        return PlayerPrefs.GetInt(appSetting.BallPowerLevel_playerprefs,1);
    }

    /// <summary>
    /// streamingAssets to persistentPath
    /// </summary>
    private void setGameDataFilesToAndroidPath()
    {
        #if UNITY_ANDROID




        #endif
    }


    private void OnRefresh(object[] param)
    {
        var data = param[0] as gamedata;

        ///小球重力  等级
        PlayerPrefs.SetInt(appSetting.BallPowerLevel_playerprefs,data.ballPowerLevel);
        ///金币收益能力  等级
        PlayerPrefs.SetInt(appSetting.GoldMultipleLevel_playerprefs, data.GoldMulitipleLevel);
        /// 设置全局金币数量
        PlayerPrefs.SetInt(appSetting.goldDataName_PlayerPrefs, data.GameGoldValue);
        /// 更新全局击杀AI球的数量
        PlayerPrefs.SetInt(appSetting.killAINum_playerprefs, data.GameKillAIValue);
        ///更新当局游戏进度
        PlayerPrefs.SetFloat(appSetting.curGameProgress_playerprefs, data.curGameProgressValue);

        ///更新全局游戏进度
        if (data.GameProgressValue > PlayerPrefs.GetFloat(appSetting.gameProgress_playerprefs, 0))
            PlayerPrefs.SetFloat(appSetting.gameProgress_playerprefs, data.GameProgressValue);

        ///游戏复活次数
        PlayerPrefs.SetInt(appSetting.reviveNum_playerprefs, data.iReviveNum);

        PlayerPrefs.SetInt(appSetting.loginDayNum_playerprefs, data.iLoginDayNum);

        serCurChooseBallSkinId(gamedatas.curChooseBallSkinId);
    }

    void init()
    {
        getMapCompenentInfo();
        getBallSkillsData();
        getGoldIncomeUpgradeData();
        getBallTasksInfo();
        getAIBallData();


    }

    /// <summary>
    /// 获取地图信息
    /// </summary>
    public static  List<MapData> getMapDataInfo()
    {
        return JsonDataTool.GetListFromJson<MapData>(appSetting.mapDataTableName);
  
    }



 

    /// <summary>
    /// 获取金币数量
    /// </summary>
    /// <returns></returns>
    public static int getGoldNum()
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
    /// 

   
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
                        taskProgress = 1,
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
                    });
                }
            
            }
            ///创建可修改数据表
            ///
            JsonDataTool.SetJsonFromList<ballTask>(appSetting.ballSkinGameDataTableName, ballTasksGameData);
        }
    }

    private void setBallSkinIdToBallSkinGamedataTable(int choosedSkinId )
    {
        List<ballTask> data = JsonDataTool.GetListFromJson<ballTask>(appSetting.ballSkinGameDataTableName);
        foreach (var item in data)
        {
            if (item.id == choosedSkinId)
            {
                item.isSelected = 1;
            }
            else
            {
                item.isSelected = 0;
            }
        }
        JsonDataTool.SetJsonFromList<ballTask>(appSetting.ballSkinGameDataTableName, data);
    }

    /// <summary>
    /// 获取并且修改小球任务信息表
    /// </summary>
    public List<ballTask> get_change_BallSkinTaskGameData()
    {
        List<ballTask> data = JsonDataTool.GetListFromJson<ballTask>(appSetting.ballSkinGameDataTableName);

        foreach (var item in data)
        {
           switch(item.id)
            {
                case 0:
                    item.taskProgress = 1;
                    break;
                case 1:
                    if(gamedatas.iLoginDayNum/3>=1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.iLoginDayNum / 3, 2);
                    }
                    
                    break;
                case 2:
                    if (gamedatas.iLoginDayNum / 5 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.iLoginDayNum / 5, 2);
                    }
                    
                    break;
                case 3:
                    if (gamedatas.iLoginDayNum / 7 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.iLoginDayNum / 7, 2);
                    }                    
                    break;
                case 4:
                    if (gamedatas.iReviveNum / 10 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.iReviveNum / 10, 2);
                    }
                    break;
                case 5:
                    if (gamedatas.iReviveNum / 20 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.iReviveNum / 20, 2);
                    }
                   
                    break;
                case 6:
                    if (gamedatas.GameProgressValue / 10 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.GameProgressValue / 10, 2);
                    }
                    break;
                case 7:
                    if (gamedatas.GameProgressValue / 30 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.GameProgressValue / 30, 2);
                    }
                    break;
                case 8:

                    if (gamedatas.GameProgressValue / 50 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.GameProgressValue / 50, 2);
                    }
                    break;
                case 9:
                    if (gamedatas.GameProgressValue / 80 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.GameProgressValue / 80, 2);
                    }
                    break;
                case 10:
                    if (gamedatas.GameKillAIValue / 10 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.GameKillAIValue / 10, 2);
                    }
                    break;
                case 11:

                    if (gamedatas.iNOT_KillAI_InFirst10P / 1 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = 0;
                    }


                    
                    break;
                case 12:
                    if (gamedatas.GameProgressValue / 100 >= 1)
                    {
                        item.taskProgress = 1;
                    }
                    else
                    {
                        item.taskProgress = System.Math.Round((double)gamedatas.GameProgressValue / 100, 2);
                    }
                    break;

            }
           
        }

        JsonDataTool.SetJsonFromList<ballTask>(appSetting.ballSkinGameDataTableName, data);

        return data;
    }

    /// <summary>
    /// 根据小球的皮肤ID获取相应的材质球
    /// </summary>
    /// <param name="skinId"></param>
    /// <returns></returns>
    public static Material getBallSkinForSkinId(int skinId = 0)
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

    public static int getCurChooseBallSkinId()
    {
        return PlayerPrefs.GetInt(appSetting.BallSkinId_playerprefs,0) ;
    }

    private void serCurChooseBallSkinId(int choosedId)
    {
        PlayerPrefs.SetInt(appSetting.BallSkinId_playerprefs, choosedId);
    }


    /// <summary>
    /// 根据AI球的等级获取AI的数据
    /// </summary>
    /// <param name="_level"></param>
    public AIBallData getAIBallDataForLevel(int _level)
    {
        foreach (var item in AIBallDatas)
        {
            if (item.Level == _level)
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

/// <summary>
/// 数据类型
/// </summary>
public enum DataType
{
    
    _gamedata = 1


}

/// <summary>
/// 游戏状态
/// </summary>
public enum GAMESTATE
{
    start = 0,
    game,
    over
}


public class gamedata : DataBase
{
    /// <summary>
    /// 当局游戏金币数量
    /// </summary>
    public int curGameGoldValue;

    /// <summary>
    /// 当局游戏击杀AI球的数量
    /// </summary>
   public int curGameKillAIValue;

    /// <summary>
    /// 当局游戏进度
    /// </summary>
    public float curGameProgressValue;

    /// <summary>
    /// 全局金币数量
    /// </summary>
    public int GameGoldValue;

    /// <summary>
    /// 全局击杀AI球的数量
    /// </summary>
    public int GameKillAIValue;


    /// <summary>
    /// 全局游戏进度
    /// </summary>
    public float GameProgressValue;

    /// <summary>
    /// 金币升级能力等级
    /// </summary>
    public int GoldMulitipleLevel;

    /// <summary>
    /// 小球降速等级
    /// </summary>
    public int ballPowerLevel;

    /// <summary>
    /// 完成地图前10 并且 没有击杀AI
    /// </summary>
    public int iNOT_KillAI_InFirst10P;

    /// <summary>
    /// 游戏复活次数
    /// </summary>
    public int iReviveNum;

    /// <summary>
    /// 游戏登陆天数
    /// </summary>
    public int iLoginDayNum;

    /// <summary>
    /// 游戏状态
    /// </summary>
    public GAMESTATE gameState;

    /// <summary>
    /// 总的组件数量
    /// </summary>
    public int mapComponentsNum;

    /// <summary>
    /// 地图加载进度  已经加载的组件个数 - 
    /// </summary>
    public int loadedComponentNum;

    /// <summary>
    /// 地图加载进度  MapRating -  总组数
    /// </summary>
    public int MapRating;

    /// <summary>
    /// 当前选择的小球皮肤的ID
    /// </summary>
    public int curChooseBallSkinId;

    /// <summary>
    /// 音效开关0  关 1 开
    /// </summary>
    public int sound;

    /// <summary>
    /// 震动开关  0  关 1 开
    /// </summary>
    public int vibrate;

    public override void OnInit()
    {
        loadedComponentNum = 0;
        curGameKillAIValue = 0;
        curGameGoldValue = 0;
        curGameProgressValue = 0;
        GameGoldValue = GameDataManager.getGoldNum();
        GameKillAIValue = GameDataManager.getKillAINum_total();
        GameProgressValue = GameDataManager.getGameProgress();
        GoldMulitipleLevel = GameDataManager.getGoldMultipleLevel();
        ballPowerLevel = GameDataManager.getBallPowerLevel();
        iNOT_KillAI_InFirst10P = GameDataManager.getNOT_KillAI_InFirst10P();
        iReviveNum = GameDataManager.getReviveNum();
        iLoginDayNum = GameDataManager.getLoginDayNum();
        gameState = GAMESTATE.start;
        mapComponentsNum = GameDataManager.getMapComponentNum();
        curChooseBallSkinId = GameDataManager.getCurChooseBallSkinId();
        sound = 1;
        vibrate = 1;
    }

    public override void Notify()
    {
        EventManager._instance.Invoke((int)InDataType, this);
    }

    public override DataType InDataType
    {

        get { return DataType._gamedata; }


    }
}
