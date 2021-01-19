using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class componentControl : MonoBehaviour
{
    private gamedata gamedatas;
    public int mapID = 0;
    /// <summary>
    /// AI球等级
    /// </summary>
    public string AILevel;


    /// <summary>
    /// AI球生成概率
    /// </summary>
    public string AIRating;

    /// <summary>
    /// 是否生成障碍球
    /// </summary>
    public int genAI;

    GameObject AIBall;






    private void Start()
    {
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);
        cloneAI();
        foreach (var item in transform.GetComponentsInChildren<Transform>())
        {
            if(item.gameObject.tag == "gameover")
            {
                item.gameObject.AddComponent<gameoverCube>();
                item.gameObject.GetComponent<gameoverCube>().mapComponent = transform;
            }
        }
       
    }

    private void OnRefresh(object[] param)
    {
        if(gamedatas.gameState == GAMESTATE.over)
        {
            if(mapID == gamedatas.beforeGameOverMapID - 1 || mapID == gamedatas.beforeGameOverMapID  || mapID == gamedatas.beforeGameOverMapID +1 )
            {
                if(AIBall != null)
                Destroy(AIBall);
            }
        }
    }

    void cloneAI()
    {
        //if(transform.GetComponent<Animator>()== null  && AIBall == null && mapID != 0)
        if(genAI == 1)
        {
            string[] strs_aiLevel = AILevel.Split('|');
            string[] strs_aiRating = AIRating.Split('|');

            int randonValue = ConvertHelper.getRandomNumber();

            AIBall = Instantiate(GameDataManager._instance.AIObject) as GameObject; 

            if (randonValue >= 0 && randonValue < float.Parse(strs_aiRating[0]) * 100)
            {
                AIBall.GetComponent<AIBallControl>().ballData = GameDataManager._instance.getAIBallDataForLevel(int.Parse(strs_aiLevel[0]));
            }
            else if (randonValue >= float.Parse(strs_aiRating[0]) * 100 && randonValue < (float.Parse(strs_aiRating[0]) * 100 + float.Parse(strs_aiRating[1]) * 100))
            {
                AIBall.GetComponent<AIBallControl>().ballData = GameDataManager._instance.getAIBallDataForLevel(int.Parse(strs_aiLevel[1]));

            }
            else if (randonValue >= (float.Parse(strs_aiRating[0]) * 100 + float.Parse(strs_aiRating[1]) * 100) && randonValue < (float.Parse(strs_aiRating[0]) * 100 + float.Parse(strs_aiRating[1]) * 100 + float.Parse(strs_aiRating[2]) * 100))
            {
                AIBall.GetComponent<AIBallControl>().ballData = GameDataManager._instance.getAIBallDataForLevel(int.Parse(strs_aiLevel[2]));

            }


            AIBall.transform.parent = transform;
            AIBall.transform.localPosition = new Vector3(0, 0.87f, 0);
        }
        

    }
 


}
