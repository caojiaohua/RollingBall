using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using zFrame.UI;

public class JoystickPanel : BasePanel
{
    public static  JoystickPanel _instance;
    public Joystick joystick;

    public Text txt_gameProgress;
    public Text txt_KillAInNum;

    private gamedata gamedatas;
    private void Start()
    {
        _instance = this;



    }

    private void OnRefresh(object[] param)
    {
        txt_KillAInNum.text = gamedatas.curGameKillAIValue.ToString();
        txt_gameProgress.text = gamedatas.curGameProgressValue.ToString("F2")+" %";
    }

    public override void OnEnter()
    {
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);

        gameObject.SetActive(true);
        joystick = transform.GetComponent<Joystick>();
        move ball = GameObject.Find("ball").GetComponent<move>();
        joystick.OnValueChanged.AddListener(ball.onJoystickValueChanged);

        ///初始化信息显示
        txt_KillAInNum.text = gamedatas.curGameKillAIValue.ToString();
        txt_gameProgress.text = gamedatas.curGameProgressValue.ToString();
    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
    }
}
