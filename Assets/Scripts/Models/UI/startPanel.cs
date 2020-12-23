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


    private void Start()
    {
        btnSetting.onClick.AddListener(OnSettingButtonClick);
        btnTask.onClick.AddListener(OnTaskButtonClick);
        btnStart.onClick.AddListener(OnStartButtonClick);
        btnUpgradeCoin.onClick.AddListener(btnUpgradePowerClick);
        btnUpgradePower.onClick.AddListener(btnUpgradePowerClick);
        canvasGroup = transform.GetComponent<CanvasGroup>();
    }

    void OnDestory()
    {
        btnSetting.onClick.RemoveListener(OnSettingButtonClick);
        btnTask.onClick.RemoveListener(OnTaskButtonClick);
        btnStart.onClick.RemoveListener(OnStartButtonClick);
        btnUpgradeCoin.onClick.RemoveListener(btnUpgradePowerClick);
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
    private void btnUpgradeCoinClick()
    {
    }
    private void btnUpgradePowerClick()
    {
        
    }
    public override void OnEnter()
    {
        ///开始界面的数据 更新
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