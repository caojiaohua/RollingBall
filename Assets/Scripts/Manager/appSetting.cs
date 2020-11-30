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


    #region  等级颜色
    public static Color level_A = Color.red;//new Color(136f/255f,77/255f,126f/255f,255f/255f);
    public static Color level_B = Color.magenta;
    public static Color level_C = Color.yellow;
    public static Color level_D = Color.green;
    public static Color level_S = Color.blue;
    public static Color level_Splus = Color.cyan;
    #endregion
}
