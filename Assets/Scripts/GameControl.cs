﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{


    List<GameObject> loadedComponent;
    public static GameControl _instance;
    /// <summary>
    /// 地图组件的父物体
    /// </summary>
    public Transform mapComponentParent;


    /// <summary>
    /// 上一个组件
    /// </summary>
    Transform lastComponent;

    List<MapData> mapData;

    private gamedata gamedatas;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;

        Init();
        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);

        setGameMap();
    }

    private void OnRefresh(object[] param)
    {
        var data = param[0] as gamedata;
        

    }

    /// <summary>
    /// 重置游戏-
    /// </summary>
    public void Init()
    {

        if(loadedComponent != null)
        {
            foreach (var item in loadedComponent)
            {
                Destroy(item);
            }
        }
        loadedComponent = new List<GameObject>();

        gamedatas.loadedComponentNum = 0;
        gamedatas.Notify();
    }
    
  
    /// <summary>
    /// 加载地图   分段加载   2
    /// </summary>
   public void setGameMap()
    {
        if(gamedatas.isTest == false)mapData = GameDataManager._instance.getMapDataInfo();
        else mapData = GameDataManager._instance.getTestMapDataInfo();

        if (gamedatas.MapRating == 0)
        {
            Init();
            GameObject startPoint = checkMapComponentList(GameDataManager._instance.startPointObject);

            startPoint.transform.localPosition = new Vector3(0,0,0);

            lastComponent = startPoint.transform;

            startPoint.GetComponent<componentControl>().mapID = 0;
        }
        if(gamedatas.MapRating < mapData.Count)
        {
            for (int i = gamedatas.MapRating; i < gamedatas.MapRating + 1; i++)
            {
                int gerenalComponent = mapData[i].gernaralComponentRating;
                int lowerComponent = mapData[i].lowerCompnenetRating;
                int middleComponent = mapData[i].middleComponentRating;
                int seniorComponent = mapData[i].seniorComponentRating;
                for (int j = 0; j < mapData[i].compenentNumb; j++)
                {
                    int randomNum = ConvertHelper.getRandomNumber();
                    componentInfo xx = null;
                    if (randomNum >= 0 && randomNum < gerenalComponent)
                    {
                        xx = GameDataManager._instance.getGeneralComponentObject();
                    }
                    else if (randomNum >= gerenalComponent && randomNum < (lowerComponent + gerenalComponent))
                    {
                        xx = GameDataManager._instance.getLowerComponentObject();

                    }
                    else if (randomNum >= (gerenalComponent + lowerComponent) && randomNum < (lowerComponent + gerenalComponent + middleComponent))
                    {
                        xx = GameDataManager._instance.getMiddleComponentObject();

                    }
                    else if (randomNum >= (gerenalComponent + lowerComponent + middleComponent) && randomNum < (lowerComponent + gerenalComponent + middleComponent + seniorComponent))
                    {
                        xx = GameDataManager._instance.getSeniorComponentObject();

                    }
                    if (xx == null)
                    {
                        Debug.Log(xx.obj.name);
                    }
                    GameObject compoment = checkMapComponentList(xx.obj);

                    compoment.GetComponent<componentControl>().mapID = j + gamedatas.loadedComponentNum + 1;
                    compoment.GetComponent<componentControl>().AILevel = mapData[i].AILevel;
                    compoment.GetComponent<componentControl>().AIRating = mapData[i].AIRating;
                    compoment.GetComponent<componentControl>().genAI = xx.genAI;


                    compoment.name = (j + gamedatas.loadedComponentNum + 1).ToString();

                    compoment.transform.localPosition = getEndPoint(lastComponent).position - getStartPoint(compoment.transform).position;

                    lastComponent = compoment.transform;

                }

                gamedatas.loadedComponentNum += mapData[i].compenentNumb;
                gamedatas.Notify();
            }
        }
         if (gamedatas.MapRating == mapData.Count)
        {
            GameObject endPoint = checkMapComponentList(GameDataManager._instance.endPointObject);

            endPoint.transform.localPosition = getEndPoint(lastComponent).position - getStartPoint(endPoint.transform).position;
        }


    }

    /// <summary>
    /// 保持总共不超过50个组件
    /// </summary>
    GameObject checkMapComponentList(Object _object)
    {
         GameObject _gameobject =  Instantiate(_object) as GameObject;

        _gameobject.transform.parent = mapComponentParent;
        loadedComponent.Add(_gameobject);

        if (loadedComponent.Count > 55)
        {
            Destroy(loadedComponent[0]);
            loadedComponent.RemoveAt(0);
        }

        return _gameobject;
    }


    Transform getStartPoint(Transform parent)
    {
        Transform[] xx = parent.gameObject.GetComponentsInChildren<Transform>();
        for(int i = 0;i<xx.Length;i++)
        {
            if(xx[i].tag == "startPoint")
            {
                return xx[i];
            }
        }
        return null;
    }

    Transform getEndPoint(Transform parent)
    {
        Transform[] xx = parent.gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < xx.Length; i++)
        {
            if (xx[i].tag == "endPosition")
            {
                return xx[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 玩家退出游戏的时候回收内存
    /// </summary>
    private void OnApplicationQuit()
    {

        ///清空asset内存
        ///
        //GameDataManager._instance.clearAllObject();

        //foreach (var item in loadedComponent)
        //{
        //    Destroy(item);
        //}


      

        //System.GC.Collect();
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

}
