using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverPanel : BasePanel
{
    /// <summary>
    /// 复活确认界面
    /// </summary>
    public GameObject continuePanel;
    /// <summary>
    /// 看广告 复活
    /// </summary>
    public Button btn_continue_watchAds;
    /// <summary>
    /// 不看广告
    /// </summary>
    public Button btn_continue_noAds;

    /// <summary>
    /// 结算界面
    /// </summary>
    public GameObject overPanel;


    public Text txt_gameProgress;

    public Text txt_goldNum;

    public Button btn_over_DoubleGold;

    public Button btn_over_NoAds;

    GameObject ball;

    private gamedata gamedatas;
    private void Awake()
    {
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);


        ball = GameObject.Find("ball").transform.GetChild(0).gameObject;
        btn_continue_watchAds.onClick.AddListener(btn_continue_watchAdsClick);
        btn_continue_noAds.onClick.AddListener(btn_continue_noAdsClick);

        btn_over_DoubleGold.onClick.AddListener(btn_over_DoubleGoldClick);
        btn_over_NoAds.onClick.AddListener(btn_over_NoAdsClick);
    }

    private void OnRefresh(object[] param)
    {
        
    }

    /// <summary>
    /// 游戏结束界面的 no ads按钮 事件
    /// </summary>
    void btn_over_NoAdsClick()
    {
        UIPanelManager.Instance.PushPanel(UIPanelType.start);
        gamedatas.MapRating = 0;

        gamedatas.curGameProgressValue = 0;
        gamedatas.Notify();
        GameControl._instance.setGameMap();
        
        ball.GetComponent<ballContro>().resetPosition();
        
    }

    /// <summary>
    /// 游戏结束界面的 watch ads 按钮事件
    /// </summary>
    void btn_over_DoubleGoldClick()
    {
        gamedatas.GameGoldValue += gamedatas.curGameGoldValue;

        gamedatas.curGameGoldValue += gamedatas.curGameGoldValue;

        gamedatas.MapRating = 0;

        gamedatas.curGameProgressValue = 0;
        gamedatas.Notify();
        GameControl._instance.setGameMap();
        ball.GetComponent<ballContro>().resetPosition(false);
        UIPanelManager.Instance.PushPanel(UIPanelType.start);

        
    }
    /// <summary>
    /// 复活界面  观看广告按钮
    /// </summary>
    void btn_continue_watchAdsClick()
    {
        ball.GetComponent<ballContro>().resetPosition(true);
        gamedatas.gameState = GAMESTATE.start;
        gamedatas.iReviveNum += 1;
        UIPanelManager.Instance.PushPanel(UIPanelType.joystick);
        gamedatas.Notify();


    }

    /// <summary>
    /// 复活界面  no ads 按钮  no thanks
    /// </summary>
    void btn_continue_noAdsClick()
    {
        continuePanel.gameObject.SetActive(false);
        overPanel.gameObject.SetActive(true);

        txt_gameProgress.text = gamedatas.curGameProgressValue.ToString() + "%";

        txt_goldNum.text = gamedatas.curGameGoldValue.ToString();


    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        if(gamedatas.curGameProgressValue == 100)
        {
            continuePanel.SetActive(false);
            overPanel.SetActive(true);
        }
        else
        {
            continuePanel.SetActive(true);
            overPanel.SetActive(false);
        }
        if(gamedatas.sound == 1)
        gameObject.GetComponent<AudioSource>().Play();

    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        gameObject.SetActive(true);
        continuePanel.SetActive(true);
    }

    public override void OnExit()
    {
    }
}