using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnSettingPanel_Close;
    public Button btnShop;
    public Button btnShopPanel_Close;

    /// <summary>
    /// 控制器
    /// </summary>
    public GameObject joystickControl;

    /// <summary>
    /// 游戏结束面板
    /// </summary>
    public GameObject gameOverPanel;

    /// <summary>
    /// 设置面板
    /// </summary>
    public GameObject settingPanel;


    /// <summary>
    /// 商城
    /// </summary>
    public GameObject shopPanel;

    /// <summary>
    /// 开始界面
    /// </summary>
    public GameObject startPanel;



    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(btnStartClick);
        btnSetting.onClick.AddListener(btnSettingClick);
        btnSettingPanel_Close.onClick.AddListener(btnSettingPanelCloseClick);
        btnShop.onClick.AddListener(btnShopClick);
        btnShopPanel_Close.onClick.AddListener(btnShopPanelCloseClick);

        init();
    }

    void init()
    {
        gameOverPanel.SetActive(false);
        settingPanel.SetActive(false);
        shopPanel.SetActive(false);
        startPanel.SetActive(true);
    }
    /// <summary>
    /// 开始游戏按钮
    /// </summary>
    public void btnStartClick()
    {

    }
    /// <summary>
    /// 商店按钮
    /// </summary>
    public void btnShopClick()
    {
        setShopPanel(true);
    }
    /// <summary>
    /// 商店界面 -关闭按钮
    /// </summary>
    public void btnShopPanelCloseClick()
    {
        setShopPanel(false);
    }

    /// <summary>
    /// 设置按钮
    /// </summary>
    public void btnSettingClick()
    {
        setSettingPanel(true);
    }
    /// <summary>
    /// 设置界面- 关闭按钮
    /// </summary>
    public void btnSettingPanelCloseClick()
    {
        setSettingPanel(false);

    }



    public void setSettingPanel(bool isShow)
    {
        settingPanel.SetActive(isShow);
    
    }

    public void setShopPanel(bool isShow)
    {
        shopPanel.SetActive(isShow);
    }


    public void setStartPanel(bool isShow )
    {
        startPanel.SetActive(isShow);
    }

    /// <summary>
    /// 设置滑动面板的显示
    /// </summary>
    /// <param name="isShow"></param>
    public void setJoystickPanel(bool isShow)
    {
        joystickControl.SetActive(isShow);
    }


    





}
