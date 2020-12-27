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
    public Text txt_killAINum;
    public Text txt_goldNum;

    public Button btn_over_DoubleGold;

    public Button btn_over_NoAds;

    GameObject ball;

    private gamedata gamedatas;
    private void Awake()
    {
        //gamedatas = new gamedata();
        Debug.Log(gameObject.name);
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
        ball.GetComponent<ballContro>().resetPosition();
        
    }

    /// <summary>
    /// 游戏结束界面的 watch ads 按钮事件
    /// </summary>
    void btn_over_DoubleGoldClick()
    {
        ball.GetComponent<ballContro>().resetPosition(false);
        UIPanelManager.Instance.PushPanel(UIPanelType.start);

        
    }
    /// <summary>
    /// 复活界面  观看广告按钮
    /// </summary>
    void btn_continue_watchAdsClick()
    {
        ball.GetComponent<ballContro>().resetPosition(true);
        UIPanelManager.Instance.PushPanel(UIPanelType.joystick);


    }

    /// <summary>
    /// 复活界面  no ads 按钮  no thanks
    /// </summary>
    void btn_continue_noAdsClick()
    {
        continuePanel.gameObject.SetActive(false);
        overPanel.gameObject.SetActive(true);

        txt_gameProgress.text = gamedatas.curGameProgressValue.ToString() + "%";
        txt_killAINum.text = gamedatas.curGameKillAIValue.ToString();
        txt_goldNum.text = gamedatas.curGameGoldValue.ToString();


    }

    public override void OnEnter()
    {
        continuePanel.SetActive(true);
        overPanel.SetActive(false);
    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        continuePanel.SetActive(true);
    }

    public override void OnExit()
    {
    }
}