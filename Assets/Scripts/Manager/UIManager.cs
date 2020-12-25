using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text txt_gold;


    // Start is called before the first frame update
    void Start()
    {

        UIPanelManager panelManager = UIPanelManager.Instance;
        panelManager.PushPanel(UIPanelType.start);
        set_txt_gold();
    }

    public void set_txt_gold()
    {
        txt_gold.text = GameDataManager._instance.getGoldNum().ToString();
    }

}
