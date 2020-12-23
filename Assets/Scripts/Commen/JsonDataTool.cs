using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using System;


/*
 * 
 * litjson 要求json数据的属性名要和实体类中的变量名一一对应
 */
public class JsonDataTool
{

    public static List<T> GetListFromJson<T>(string path)
    {//获取json数据
        string str = File.ReadAllText(appSetting.dataPath + path+".json", Encoding.GetEncoding("UTF-8"));//读取Json字符串
        if (str == null) Debug.LogError("未找到目标资源:" + path+".json");
        List<T> list = JsonUtility.FromJson<Serialization<T>>(str).ToList();
        return list;
    }

    public static void SetJsonFromList<T>(string path, List<T> list)//此方法用于存档的实现
    {//修改json数据
        string jsonstr = JsonMapper.ToJson(list);
        File.WriteAllText(appSetting.dataPath + path + ".json","{"+'"'+"target"+'"'+":"+ jsonstr +"}");//  {"target": [      ////      	]}
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

