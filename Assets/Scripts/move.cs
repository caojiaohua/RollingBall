using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private gamedata gamedatas;
    private float moveSpeed;

    private void Start()
    {


        //gamedatas = new gamedata();
        Debug.Log(gameObject.name);
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);
    }

    private void OnRefresh(object[] param)
    {
        moveSpeed = (float)GameDataManager._instance.getBallSkillForLevel(gamedatas.ballPowerLevel).speed;
    }

    public void onJoystickValueChanged(Vector2 eventPointerData)
    {
#if UNITY_IOS || UNITY_ANDROID
        float angle = Vector2.SignedAngle(new Vector2(1, 0), eventPointerData);
        
        
        if (angle != 0)
        {
            gamedatas.gameState = GAMESTATE.game;
            gamedatas.Notify();

            if (angle < 22.5f && angle > -22.5f)
            {
                //右
                RotateMove(new Vector3(0, -90, 0));

            }
            else if (angle < -22.5f && angle > -67.5f)
            {
                //右下
                RotateMove(new Vector3(0, -45, 0));

            }
            else if (angle < -67.5f && angle > -112.5f)
            {
                //下
                RotateMove(new Vector3(0, 0, 0));

            }
            else if (angle < -112.5f && angle > -157.5f)
            {
                //左下
                RotateMove(new Vector3(0, 45, 0));

            }
            else if (angle < -157.5f || angle > 157.5f)
            {
                //左
                RotateMove(new Vector3(0, 90, 0));

            }
            else if (angle < 157.5f && angle > 112.5f)
            {
                //左上
                RotateMove(new Vector3(0, 135, 0));

            }
            else if (angle < 112.5f && angle > 67.5f)
            {
                //上
                RotateMove(new Vector3(0, 180, 0));

            }
            else if (angle < 67.5f && angle > 22.5f)
            {
                //右上
                RotateMove(new Vector3(0, -135, 0));
            }
        }

#endif
    }

    private void Update()
    {
        if(gamedatas.gameState == GAMESTATE.game)
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }


    private void RotateMove(Vector3 vet3)
    {
        transform.eulerAngles = vet3;
    }


}
