using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraContorl : MonoBehaviour
{

     GameObject ball;
    public float cameraY = 8.25f;

    private gamedata gamedatas;
    public float cameraZ = 8.3f;

    public void Start()
    {
        ball = GameObject.Find("ball");
        //gamedatas = new gamedata();
        Debug.Log(gameObject.name);
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);
    }

    private void OnRefresh(object[] param)
    {
        
    }

    void Update()
    {
        if(gamedatas.gameState != GAMESTATE.over)
         Camera.main.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + cameraY, ball.transform.position.z + cameraZ);
    }
}
