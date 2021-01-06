using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using System;
using UnityEngine.Networking;


/*
 * 
 * litjson 要求json数据的属性名要和实体类中的变量名一一对应
 */
public class JsonDataTool
{

    public static List<T> GetListFromJson<T>(string path)
    {
        string str = "";
#if UNITY_ANDROID

        UnityWebRequest reader =  UnityWebRequest.Get(appSetting.dataPath + path + ".json");

        reader.SendWebRequest();
        while (!reader.isDone){}
        str = reader.downloadHandler.text;

#elif UNITY_EDITOR

        str = File.ReadAllText(appSetting.dataPath + path+".json", Encoding.GetEncoding("UTF-8"));//读取Json字符串
#endif
        //获取json数据

        if (str == null) Debug.LogError("未找到目标资源:" + path+".json");
        List<T> list = JsonUtility.FromJson<Serialization<T>>(str).ToList();
        return list;
    }

    public static void SetJsonFromList<T>(string path, List<T> list)//此方法用于存档的实现
    {//修改json数据
        string jsonstr = JsonMapper.ToJson(list);
#if UNITY_ANDROID

        File.WriteAllText(Application.persistentDataPath + "/" + path + ".json", "{" + '"' + "target" + '"' + ":" + jsonstr + "}");//  {"target": [      ////      	]}s

#elif UNITY_EDITOR

        File.WriteAllText(appSetting.dataPath  +path + ".json","{"+'"'+"target"+'"'+":"+ jsonstr +"}");//  {"target": [      ////      	]}s
#endif




    }

    public static bool isFileExit()
    {
        return false;
    }

}

[Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

