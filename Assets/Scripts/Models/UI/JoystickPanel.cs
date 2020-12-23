using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zFrame.UI;

public class JoystickPanel : BasePanel
{
    public Joystick joystick;

    public override void OnEnter()
    {
        joystick = transform.GetComponent<Joystick>();
        move ball = GameObject.Find("ball").GetComponent<move>();
        joystick.OnValueChanged.AddListener(ball.onJoystickValueChanged);
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
}
