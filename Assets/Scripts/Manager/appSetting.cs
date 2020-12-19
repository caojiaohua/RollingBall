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
    

    public static int initGoldNum = 0;
    public static int initDiamondNum = 0;


    public static string mapDataTableName = "levelData";
    public static string mapComponentTableName = "mapCompoment";
    public static string MATTableName = "allMAT";
    public static string propTableName = "prop";
    public static string ornamentTableName = "ornament";



}
