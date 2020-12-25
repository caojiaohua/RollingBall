using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startPanel : BasePanel
{
    public Button btnSetting;
    public Button btnTask;
    public Button btnStart;
    private CanvasGroup canvasGroup;
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

    private void Start()
    {
        btnSetting.onClick.AddListener(OnSettingButtonClick);
        btnTask.onClick.AddListener(OnTaskButtonClick);
        btnStart.onClick.AddListener(OnStartButtonClick);
        btnUpgradeCoin.onClick.AddListener(btnUpgradeCoinClick);
        btnUpgradePower.onClick.AddListener(btnUpgradePowerClick);
        canvasGroup = transform.GetComponent<CanvasGroup>();
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

    }

    /// <summary>
    /// 升级小球重力
    /// </summary>
    private void btnUpgradePowerClick()
    {
        
    }
    public override void OnEnter()
    {
        GameDataManager._instance.gameState = GAMESTATE.start;
        gameObject.SetActive(true);
        ///开始界面的数据 更新
        ///
        int goldLevel = GameDataManager._instance.getGoldMultipleLevel();
        int ballPowerLevel = GameDataManager._instance.getBallPowerLevel();
        txt_goldLevel.text = goldLevel.ToString();
        txt_goldUpgradePrice.text = GameDataManager._instance.getGoldUpgradeForLevel(goldLevel).price.ToString();
        txt_powerLevel.text = ballPowerLevel.ToString();
        txt_powerUpgradePrice.text = GameDataManager._instance.getBallSkillForLevel(ballPowerLevel).price.ToString();

    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        
    }
}