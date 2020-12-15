using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    Vector3 nowPosition;
    // Start is called before the first frame update
    void Start()
    {
        nowPosition = new Vector3(0, 0, 0);
        setGameMap();
    }

    void setGameMap()
    {
        int curGameLevel = GameDataManager._instance.getCurGameLevel();

        LevelData curLevelData = GameDataManager._instance.getMapInfoForID(curGameLevel);
      
        string[] curLevelValue = curLevelData.mapValue.Split('|');
        for (int i = 0; i < curLevelValue.Length; i++)
        {
            ///根据ID获取resource name
            ///
            Object xx = GameDataManager._instance.getMapCompomentForId(int.Parse(curLevelValue[i]));
            GameObject compoment = Instantiate(xx) as GameObject;
            nowPosition += new Vector3(0, 0, (float)System.Math.Round(compoment.transform.GetComponent<Renderer>().bounds.size.z / 1, 2));
            compoment.transform.localPosition = new Vector3(nowPosition.x, nowPosition.y, nowPosition.z);
        }
    }
   
}
