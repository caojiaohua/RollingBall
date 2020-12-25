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

    private void Awake()
    {
        _instance = this;
    }
    public override void OnEnter()
    {
        GameDataManager._instance.gameState = GAMESTATE.game;
        joystick = transform.GetComponent<Joystick>();
        move ball = GameObject.Find("ball").GetComponent<move>();
        joystick.OnValueChanged.AddListener(ball.onJoystickValueChanged);
        setKillAINum();
        setGameProgress(0);
    }

    public override void OnPause()
    {
    }

    public override void OnResume()
    {
    }

    public override void OnExit()
    {
    }

    public void  setKillAINum()
    {
        txt_KillAInNum.text = GameDataManager._instance.getKillAINum().ToString();
    }

    public  void setGameProgress(int componentID)
    {
        txt_gameProgress.text = (((float)componentID / (float)GameDataManager._instance.componentTotalNum)*100).ToString()+" %";
       
    }
}
