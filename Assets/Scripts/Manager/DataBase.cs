
using System.Collections.Generic;

public abstract class DataBase
{

    /// <summary>
    /// 数据类型
    /// </summary>
    public abstract DataType InDataType { get; }

    /// <summary>
    /// 初始化
    /// </summary>
    public abstract void OnInit();

    /// <summary>
    /// 广播自身
    /// </summary>
    public abstract void Notify();

    /// <summary>
    /// 构造函数内注册自己
    /// </summary>
    public DataBase()
    {
        OnInit();
        DataManager._instance.Register(InDataType, this);
    }
}




/// <summary>
/// des:事件封装
/// </summary>
public class DataEvent
{
    /// <summary>
    /// 实例事件
    /// </summary>
    public EventMgr InstanceEvent;

    /// <summary>
    /// 绑定方法
    /// </summary>
    /// <param name="eventMgr"></param>
    public void BindEvnt(EventMgr eventMgr)
    {
        InstanceEvent += eventMgr;
    }
}


/// <summary>
/// 数据管理成员接口
/// </summary>
public interface IDataMgr
{

    Dictionary<DataType, DataBase> EventListerDict { get; set; }//存储注册的信息

    Dictionary<DataType, DataEvent> EventMgrDict { get; set; }//存储绑定事件信息

    void AddDataWatch(DataType dataType, EventMgr eventMgr);//绑定更新方法

    void Register(DataType dataType, DataBase dataBase);//注册数据信息

    DataBase Get(DataType dataType);//获取数据

}