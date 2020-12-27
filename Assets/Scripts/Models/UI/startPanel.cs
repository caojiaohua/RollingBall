using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startPanel : BasePanel
{

    public Button btnSetting;
    public Button btnTask;
    public Button btnStart;
    
    /// <summary>
    /// 提升小球金币收益能力
    /// </summary>
    public Button btnUpgradeCoin;

    /// <summary>
    /// 提升小球自身重力
    /// </summary>
    public Button btnUpgradePower;

    public Text txt_goldLevel;
    public Text txt_goldUpgradePrice;

    public Text txt_powerLevel;
    public Text txt_powerUpgradePrice;

    private gamedata gamedatas;

    private void Start()
    {
        ////gamedatas = new gamedata();
        Debug.Log(gameObject.name);


        #region
        txt_goldLevel.text = gamedatas.GoldMulitipleLevel.ToString();
        txt_goldUpgradePrice.text = GameDataManager._instance.getGoldUpgradeForLevel(gamedatas.GoldMulitipleLevel).price.ToString();
        txt_powerLevel.text = gamedatas.ballPowerLevel.ToString();
        txt_powerUpgradePrice.text = GameDataManager._instance.getBallSkillForLevel(gamedatas.ballPowerLevel).price.ToString();
        #endregion

        
        btnSetting.onClick.AddListener(OnSettingButtonClick);
        btnTask.onClick.AddListener(OnTaskButtonClick);
        btnStart.onClick.AddListener(OnStartButtonClick);
        btnUpgradeCoin.onClick.AddListener(btnUpgradeCoinClick);
        btnUpgradePower.onClick.AddListener(btnUpgradePowerClick);
        
    }

    private void OnRefresh(object[] param)
    {
        //获取广播的数据
        var user = param[0] as gamedata;

        txt_goldLevel.text = user.GoldMulitipleLevel.ToString();
        txt_goldUpgradePrice.text = GameDataManager._instance.getGoldUpgradeForLevel(user.GoldMulitipleLevel).price.ToString();
        txt_powerLevel.text = user.ballPowerLevel.ToString();
        txt_powerUpgradePrice.text = GameDataManager._instance.getBallSkillForLevel(user.ballPowerLevel).price.ToString();

    }

    void OnDestory()
    {
        btnSetting.onClick.RemoveListener(OnSettingButtonClick);
        btnTask.onClick.RemoveListener(OnTaskButtonClick);
        btnStart.onClick.RemoveListener(OnStartButtonClick);
        btnUpgradeCoin.onClick.RemoveListener(btnUpgradeCoinClick);
        btnUpgradePower.onClick.RemoveListener(btnUpgradePowerClick);
    }



    private void OnTaskButtonClick()
    {
        UIPanelManager.Instance.PushPanel(UIPanelType.Task);
    }
    private void OnStartButtonClick()
    {
        UIPanelManager.Instance.PushPanel(UIPanelType.joystick);
    }
    private void OnSettingButtonClick()
    {
        UIPanelManager.Instance.PushPanel(UIPanelType.setting);
    }

    /// <summary>
    /// 升级金币收益能力
    /// </summary>
    private void btnUpgradeCoinClick()
    {
        ///获取升级到下一级需要多少金币
        ///
        int nextLevel_needGold = GameDataManager._instance.getGoldUpgradeForLevel(gamedatas.GoldMulitipleLevel).price;

        if(gamedatas.GameGoldValue >= nextLevel_needGold)
        {
            
            gamedatas.curGameGoldValue -= nextLevel_needGold;
            gamedatas.GoldMulitipleLevel += 1;

            
        }
        else
        {
            Debug.Log("Insufficient gold");
        }

        gamedatas.Notify();

    }

    /// <summary>
    /// 升级小球重力 
    /// </summary>
    private void btnUpgradePowerClick()
    {
        ///获取升级到下一级需要多少金币
        ///
        int nextLevel_needGold = GameDataManager._instance.getBallSkillForLevel(gamedatas.ballPowerLevel).price;

        if (gamedatas.GameGoldValue >= nextLevel_needGold)
        {
            gamedatas.ballPowerLevel += 1;
            gamedatas.GameGoldValue -= nextLevel_needGold;   
        }
        else
        {
            Debug.Log("Insufficient gold");
        }

        gamedatas.Notify();
    }
    public override void OnEnter()
    {
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);
        gamedatas.gameState = GAMESTATE.start;
        gameObject.SetActive(true);

        ///开始界面的数据 更新
        ///
        gamedatas.curGameGoldValue = 0;
        gamedatas.curGameKillAIValue = 0;



        //GameControl._instance.Init();

    }
    


    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {

    }

    public override void OnExit()
    {
        
    }
}