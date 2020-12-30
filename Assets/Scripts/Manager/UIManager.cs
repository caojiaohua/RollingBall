using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public Text txt_gold;
    public static UIManager _instance;

    private gamedata gamedatas;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        //Debug.Log(gameObject.name);
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);

        UIPanelManager panelManager = UIPanelManager.Instance;
        panelManager.PushPanel(UIPanelType.start);
        txt_gold.text = gamedatas.GameGoldValue.ToString();
    }

    private void OnRefresh(object[] param)
    {
        txt_gold.text = gamedatas.GameGoldValue.ToString();
    }


    //void btnaddClick()
    //{
    //    gamedatas.move
    //}
    //void btnadd1Click()
    //{

    //}

    //void btnReduceClick()
    //{

    //}

    //void btnReduce1Click()
    //{

    //}

}
