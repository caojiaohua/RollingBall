using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    int curMapRating = 0;

    Transform obj1;
    // Start is called before the first frame update
    void Start()
    {
         setGameMap();
    }
  
    /// <summary>
    /// 加载地图   分段加载
    /// </summary>
    void setGameMap()
    {
        List<GameObject> test = new List<GameObject>();
        List<MapData> mapData = GameDataManager._instance.mapDatas;
        if(curMapRating == 0)
        {
            GameObject startPoint = Instantiate( GameDataManager._instance.startPointObject) as GameObject;
            startPoint.transform.localPosition = new Vector3(0,0,0);
            obj1 = startPoint.transform;


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


                
                //compoment.name = (j * i+j).ToString();
                
                compoment.transform.localPosition = getEndPoint(obj1).position - getStartPoint(compoment.transform).position;


              
                obj1 = compoment.transform;
               
            }
        }

        curMapRating += 2;

        if (curMapRating == mapData.Count-1)
        {
            GameObject endPoint = Instantiate(GameDataManager._instance.endPointObject) as GameObject;
            endPoint.transform.localPosition = getEndPoint(obj1).position - getStartPoint(endPoint.transform).position;
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
