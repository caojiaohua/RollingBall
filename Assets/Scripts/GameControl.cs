using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    List<GameObject> allMapComponent;
    public static GameControl _instance;
    /// <summary>
    /// 地图组件的父物体
    /// </summary>
    public Transform mapComponentParent;

    /// <summary>
    /// 当前地图加载的进度  curMapRating / mapData.Count
    /// </summary>
    int curMapRating = 0;


    int loadedComponentMum;

    /// <summary>
    /// 上一个组件
    /// </summary>
    Transform lastComponent;

    List<MapData> mapData;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        allMapComponent = new List<GameObject>();
         setGameMap(curMapRating);

      

    }
  
    /// <summary>
    /// 加载地图   分段加载   2
    /// </summary>
    void setGameMap(int _loadMapProgress)
    {
        List<GameObject> test = new List<GameObject>();
        mapData = GameDataManager._instance.mapDatas;
        
        if(curMapRating == 0)
        {
            GameObject startPoint = Instantiate( GameDataManager._instance.startPointObject) as GameObject;
            startPoint.transform.parent = mapComponentParent;
            startPoint.transform.localPosition = new Vector3(0,0,0);
            lastComponent = startPoint.transform;


        }
        for(int i = curMapRating; i<curMapRating+2;i++)
        {
            int gerenalComponent = mapData[i].gernaralComponentRating;
            int lowerComponent = mapData[i].lowerCompnenetRating;
            int middleComponent = mapData[i].middleComponentRating;
            int seniorComponent = mapData[i].seniorComponentRating;
            for (int j = 0;j<mapData[i].compenentNumb;j++)
            {
                int randomNum = ConvertHelper.getRandomNumber();
                Object xx = null;
                if(randomNum >=0 && randomNum < gerenalComponent)
                {
                    xx = GameDataManager._instance.getGeneralComponentObject();
                }
                else if(randomNum >= gerenalComponent && randomNum < (lowerComponent+gerenalComponent))
                {
                    xx = GameDataManager._instance.getLowerComponentObject();

                }
                else if(randomNum >= (gerenalComponent+lowerComponent) && randomNum < (lowerComponent + gerenalComponent+middleComponent))
                {
                    xx = GameDataManager._instance.getMiddleComponentObject();

                }
                else if(randomNum >= (gerenalComponent + lowerComponent+middleComponent) && randomNum < (lowerComponent + gerenalComponent + middleComponent+seniorComponent))
                {
                    xx = GameDataManager._instance.getSeniorComponentObject();

                }
                if(xx ==  null)
                {
                    Debug.Log(xx.name);
                }
                GameObject compoment = Instantiate(xx) as GameObject;

                compoment.transform.parent = mapComponentParent;
                allMapComponent.Add(compoment);
                checkMapComponentList();

                compoment.GetComponent<componentControl>().mapID = i * mapData[i].compenentNumb + j;
                compoment.GetComponent<componentControl>().AILevel = mapData[i].AILevel;
                compoment.GetComponent<componentControl>().AIRating = mapData[i].AIRating;

                compoment.name = (i * mapData[i].compenentNumb + j).ToString();

                compoment.transform.localPosition = getEndPoint(lastComponent).position - getStartPoint(compoment.transform).position;



                lastComponent = compoment.transform;
               
            }
            loadedComponentMum += mapData[i].compenentNumb;
        }

        curMapRating += 2;
        
        if (curMapRating == mapData.Count-1)
        {
            GameObject endPoint = Instantiate(GameDataManager._instance.endPointObject) as GameObject;
            endPoint.transform.parent = mapComponentParent;

            endPoint.transform.localPosition = getEndPoint(lastComponent).position - getStartPoint(endPoint.transform).position;
        }


    }

    /// <summary>
    /// 保持总共不超过50个组件
    /// </summary>
    void checkMapComponentList()
    {
        if(allMapComponent.Count > 50)
        {
            Destroy(allMapComponent[0]);
            allMapComponent.RemoveAt(0);
        }
    }

    public void checkMapProgress(int mapId)
    {
        if(loadedComponentMum-mapId<=10)
        {
            //继续加载
            setGameMap(curMapRating);
        }
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

}
