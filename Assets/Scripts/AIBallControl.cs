using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBallControl : MonoBehaviour
{
    public AIBallData ballData;
    Rigidbody _rigidbody;

    private gamedata gamedatas;

    private void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
        _rigidbody.mass = (float)ballData.power;


        //gamedatas = new gamedata();
        Debug.Log(gameObject.name);
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);

    }

    private void OnRefresh(object[] param)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cube")
        {
            ///更新金币
            ///
            gamedatas.GameGoldValue += (int)(GameDataManager._instance.getGoldUpgradeForLevel(gamedatas.GoldMulitipleLevel).killEnemyIncome);
            gamedatas.curGameGoldValue += (int)(GameDataManager._instance.getGoldUpgradeForLevel(gamedatas.GoldMulitipleLevel).killEnemyIncome);

            ///更新杀敌数
            ///
            gamedatas.curGameKillAIValue += 1;
            gamedatas.GameKillAIValue += 1;


            gamedatas.Notify();
            Destroy(gameObject);

        }
        else if (other.gameObject.tag == "ball")
        {
           // Debug.Log("game over");
        }
    }
}
