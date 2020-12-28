using System.Collections.Generic;
using UnityEngine;

public delegate void EventMgr(params object[] param);

public  class EventManager : MonoBehaviour
{
    public static EventManager _instance;
    /// <summary>
    /// 存储注册好的事件
    /// </summary>
    protected readonly Dictionary<int, EventMgr> EventListerDict = new Dictionary<int, EventMgr>();

    /// <summary>
    /// 是否暂停所有的事件
    /// </summary>
    public bool IsPause = false;

    private void Awake()
    {
        _instance = this;

    }

    public void Register(int key, EventMgr eventMgr)
    {
        if (EventListerDict.ContainsKey(key))
        {
            Debug.LogError("Key:" + key + "已存在！");
        }
        else
        {
            EventListerDict.Add(key, eventMgr);
        }
    }

    public void UnRegister(int key)
    {
        if (EventListerDict != null && EventListerDict.ContainsKey(key))
        {
            EventListerDict.Remove(key);
         //   Debug.Log("移除事件：" + key);
        }
        else
        {
            Debug.LogError("Key:" + key + "不存在！");
        }
    }

    public void ClearAll()
    {
        if (EventListerDict != null)
        {
            EventListerDict.Clear();
            Debug.Log("清空注册事件！");
        }
    }

    public bool IsRegisterName(int key)
    {
        if (EventListerDict != null && EventListerDict.ContainsKey(key))
        {
            EventListerDict.Remove(key);
            Debug.Log("事件：" + key + "已注册！");
            return true;
        }
        Debug.Log("事件：" + key + "未注册！");
        return false;
    }


    public bool IsRegisterFunc(EventMgr eventMgr)
    {
        if (EventListerDict != null && EventListerDict.ContainsValue(eventMgr))
        {
            Debug.Log("事件已注册！");
            return true;
        }
        Debug.Log("事件未注册！");
        return false;
    }

    public void Invoke(int key, params object[] param)
    {
        if (!IsPause)
        {
            if (EventListerDict.ContainsKey(key))
            {
                EventListerDict[key].Invoke(param);
            }
            else
            {
                Debug.LogError("事件：" + key + "未注册！");
            }
        }
        else
        {
            Debug.LogError("所有事件已暂停！");
        }
    }


}

public interface IEventMgr
{
    void Register(int key, EventMgr eventMgr);//注册事件

    void UnRegister(int key);//解绑事件

    void ClearAll();//解绑所有事件

    bool IsRegisterName(int key);//key值是否被注册

    bool IsRegisterFunc(EventMgr eventMgr);//eventMgr是否被注册

    void Invoke(int key, params object[] param);//调用
}
