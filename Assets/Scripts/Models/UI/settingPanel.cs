using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingPanel : BasePanel
{
    public Button btnSound;
    public Button btnVibrate;
    public Button btnClose;
    private gamedata gamedatas;

    private void Start()
    {
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);

        btnSound.onClick.AddListener(btnSoundClick);
        btnVibrate.onClick.AddListener(btnVibrateClick);
        btnClose.onClick.AddListener(btnCloseClick);

        if (gamedatas.sound == 1)
        {
            btnSound.GetComponent<Image>().enabled = false;
            btnSound.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            btnSound.GetComponent<Image>().enabled = true;
            btnSound.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (gamedatas.vibrate == 1)
        {
            btnVibrate.GetComponent<Image>().enabled = false;
            btnVibrate.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            btnVibrate.GetComponent<Image>().enabled = false;
            btnVibrate.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void btnCloseClick()
    {
        UIPanelManager.Instance.PushPanel(UIPanelType.start);
    }

    /// <summary>
    /// 设置音效开关
    /// </summary>
    private void btnSoundClick()
    {
        if(gamedatas.sound == 1)
        {
            gamedatas.sound = 0;
            btnSound.GetComponent<Image>().enabled = true;
            btnSound.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            gamedatas.sound = 1;
            btnSound.GetComponent<Image>().enabled = false;
            btnSound.transform.GetChild(0).gameObject.SetActive(true);
        }

        gamedatas.Notify();
    }

    /// <summary>
    /// 设置震动模式
    /// </summary>
    private void btnVibrateClick()
    {
        if (gamedatas.vibrate == 1)
        {
            gamedatas.vibrate = 0;
            btnVibrate.GetComponent<Image>().enabled = true;
            btnVibrate.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            gamedatas.vibrate = 1;
            btnVibrate.GetComponent<Image>().enabled = false;
            btnVibrate.transform.GetChild(0).gameObject.SetActive(true);
        }

        gamedatas.Notify();
    }

    private void OnRefresh(object[] param)
    {
        
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
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