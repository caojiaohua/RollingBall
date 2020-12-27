using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appSetting : MonoBehaviour
{


#if UNITY_IOS || UNITY_ANDROID
    public static string dataPath = Application.streamingAssetsPath + "/";
#elif UNITY_EDITOR
    public static string dataPath = Application.streamingAssetsPath + "/";
#endif

    public static string goldDataName_PlayerPrefs = "goldValue";

    public static string loginDayNum_playerprefs = "loginDayNum";
    public static string loginDayTime_playerprefs = "loginDayTime";
    public static string reviveNum_playerprefs = "reviveNum";
    public static string gameProgress_playerprefs = "gameProgress"; 
    public static string curGameProgress_playerprefs = "curGameProgress";
    public static string NOT_KillAI_InFirst10P_playerprefs = "NOT_KillAI_InFirst10P"; 
    public static string killAINum_playerprefs = "killAINum";
    public static string BallPowerLevel_playerprefs = "BallPowerLevel";
    public static string GoldMultipleLevel_playerprefs = "curGoldMultipleLevel";
    public static int initGoldNum = 0;
    


    public static string mapDataTableName = "mapData";
    public static string mapComponentTableName = "mapCompoment";
    public static string goldIncomeUpgardeTableName = "goldIncomeUpgrade";
    public static string ballSkinTaskTableName = "ballSkinTask";
    public static string ballSkillsTableName = "ballSkills"; 
    public static string AIBallDataTableName = "AIBallData"; 

    public static string ballSkinGameDataTableName = "ballSkinGameData";

    public static string UIPanelTypeTableName = "UIPanelType";


}
