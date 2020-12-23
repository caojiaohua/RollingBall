using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

        UIPanelManager panelManager = UIPanelManager.Instance;
        panelManager.PushPanel(UIPanelType.start);
    }

}
