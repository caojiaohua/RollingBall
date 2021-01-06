using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    //public Button btnadd;
    //public Button btnadd1;
    //public Button btnreduce;
    //public Button btnreduce1;

    public bool isMove;
    private gamedata gamedatas;
    private float moveSpeed;

    private void Start()
    {

        isMove = true;
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);
    }

    private void OnRefresh(object[] param)
    {
        //moveSpeed = (float)GameDataManager._instance.getBallSkillForLevel(gamedatas.ballPowerLevel).speed;
    }
    Vector3 test;
    public void onJoystickValueChanged(Vector2 eventPointerData)
    {
#if UNITY_IOS || UNITY_ANDROID
        float angle = Vector2.SignedAngle(new Vector2(1, 0), eventPointerData);

        //transform.GetChild(0).GetComponent<Rigidbody>().AddForce(new Vector3(-eventPointerData.x,0, -eventPointerData.y)*1,ForceMode.Impulse);
        if (angle != 0)
        {
            if (gamedatas.gameState == GAMESTATE.start)
            {
                gamedatas.gameState = GAMESTATE.game;
                gamedatas.Notify();
            }
            //test = new Vector3(0, (-angle - 90), 0);
            RotateMove(new Vector3(0, (-angle - 90), 0));
        }

#endif
    }

    private void Update()
    {
        if (gamedatas.gameState == GAMESTATE.game )
            if(isMove == false)
            {
                if((transform.localEulerAngles.y-360 >90f || transform.localEulerAngles.y-360 < -90f))
                {
                    transform.Translate(0, 0, moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            }
            
    }


    private void RotateMove(Vector3 vet3)
    {
        if (gamedatas.gameState == GAMESTATE.game)
            transform.localEulerAngles = vet3;
    }


}
