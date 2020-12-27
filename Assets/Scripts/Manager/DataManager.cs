using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IDataMgr
{
    public static DataManager _instance;
    void Awake()
    {
        _instance = this;
    }
    public Dictionary<DataType, DataBase> EventListerDict { get ; set ; }
    public Dictionary<DataType, DataEvent> EventMgrDict { get ; set ; }

    public void AddDataWatch(DataType dataType, EventMgr eventMgr)
    {
        if (EventMgrDict == null)
        {
            EventMgrDict = new Dictionary<DataType, DataEvent>();
        }

        if (EventMgrDict.ContainsKey(dataType))
        {
            //已存在该信息的刷新方法 先移除监听 绑定方法后重新监听
            EventManager._instance.UnRegister((int)dataType);
            EventMgrDict[dataType].BindEvnt(eventMgr);
            EventManager._instance.Register((int)dataType, EventMgrDict[dataType].InstanceEvent);
        }
        else
        {
            //不存在该信息的刷新方法，需注册
            var dataEvent = new DataEvent();
            dataEvent.BindEvnt(eventMgr);
            EventMgrDict.Add(dataType, dataEvent);
            EventManager._instance.Register((int)dataType, dataEvent.InstanceEvent);
        }
    }


    public DataBase Get(DataType dataType)
    {
        if (EventListerDict.ContainsKey(dataType))
        {
            return EventListerDict[dataType];
        }
        Debug.LogError("Key:" + dataType + "不存在,获取失败！");
        return null;



    }

    public void Register(DataType dataType, DataBase dataBase)
    {
        if (EventListerDict == null)
        {
            EventListerDict = new Dictionary<DataType, DataBase>();
        }

        if (EventListerDict.ContainsKey(dataType))
        {
            Debug.LogError("Key:" + dataType + "已经被注册！");
            return;
        }
        EventListerDict.Add(dataType, dataBase);

    }

}
