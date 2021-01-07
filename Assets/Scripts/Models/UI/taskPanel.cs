using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class taskPanel : BasePanel, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Text txt_taskInfo;
    public Text txt_taskProgress;
    public Button btnSelect;
    public Button btnClose;
    public Image img_taskProgress;
    public Image img_taskProgress_bg;

    private gamedata gamedatas;

    public Transform tasksParent;
    public override void OnEnter()
    {
        gameObject.SetActive(true);
        
       
    }
    void Start()
    {
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);

        btnClose.onClick.AddListener(btnCloseClick);
        btnSelect.onClick.AddListener(btnSelectClick);
        getTask();
        init();
    }

    private void OnRefresh(object[] param)
    {
        
    }

    void OnDestory()
    {
        btnClose.onClick.RemoveListener(btnCloseClick);
        btnSelect.onClick.RemoveListener(btnSelectClick);
    }

    public override void OnPause()
    {
        //UIPanelManager.Instance.PopPanel();
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
    }

    public override void OnExit()
    {

    }
    void btnCloseClick()
    {
        UIPanelManager.Instance.PushPanel(UIPanelType.start);

    }

    void btnSelectClick()
    {
        gamedatas.curChooseBallSkinId = task.id;
        gamedatas.Notify();
        btnSelect.transform.GetChild(0).gameObject.SetActive(false);
        btnSelect.transform.GetChild(1).gameObject.SetActive(true);
    }

    float scale = 0;

    public Transform[] items;
    
    private Vector3[] itemsPosition;
    private Vector3[] itemsScale;


    //对应的屏幕位置
    public Transform ui_left;//左

    public Transform ui_middle;//中

    public Transform ui_right;//右

    private Vector3 ui_position_left;
    private Vector3 ui_position_Midlle;
    private Vector3 ui_position_right;
    ballTask task;
    void UpdateTaskInfoShow()
    {
        task = items[items.Length / 2].GetComponent<taskPfbControl>().taskInfo;
       
        if(task.id != gamedatas.curChooseBallSkinId)
        {
            if (task.taskProgress != 1)
            {
                txt_taskInfo.gameObject.SetActive(true);
                txt_taskProgress.gameObject.SetActive(true);
                img_taskProgress.gameObject.SetActive(true);
                img_taskProgress_bg.gameObject.SetActive(true);
                btnSelect.gameObject.SetActive(false);

                txt_taskInfo.text = task.taskInfo;
                txt_taskProgress.text = (task.taskProgress *100).ToString() + "%";
                img_taskProgress.fillAmount = (float)task.taskProgress / 1;
            }
            else
            {
                txt_taskInfo.gameObject.SetActive(true);
                txt_taskInfo.text = task.taskInfo;
                txt_taskProgress.gameObject.SetActive(false);
                img_taskProgress.gameObject.SetActive(false);
                img_taskProgress_bg.gameObject.SetActive(false);
                btnSelect.gameObject.SetActive(true);
                btnSelect.enabled = true;
                btnSelect.transform.GetChild(0).gameObject.SetActive(true);
                btnSelect.transform.GetChild(1).gameObject.SetActive(false);
            }
           
        }
        else
        {
            txt_taskInfo.gameObject.SetActive(true);
            txt_taskInfo.text = task.taskInfo;
            txt_taskProgress.gameObject.SetActive(false);
            img_taskProgress.gameObject.SetActive(false);
            img_taskProgress_bg.gameObject.SetActive(false);
            btnSelect.gameObject.SetActive(true);
            btnSelect.enabled = false;
            btnSelect.transform.GetChild(1).gameObject.SetActive(true);
            btnSelect.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }

    void init()
    {
        ui_position_left = ui_left.position;
        ui_position_Midlle = ui_middle.position;
        ui_position_right = ui_right.position;

        ///生成任务信息
        ///

        itemsPosition = new Vector3[items.Length];
        itemsScale = new Vector3[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            if(i == items.Length/2)
            {
                items[i].position = ui_position_Midlle;
                items[i].localScale = new Vector3(2, 2, 2);
            }
            else if(i < items.Length/2)
            {
                items[i].position = ui_position_left;
                items[i].localScale = new Vector3(1, 1, 1);
            }
            else
            {
                items[i].position = ui_position_right;
                items[i].localScale = new Vector3(1, 1, 1);


            }
            itemsPosition[i] = items[i].position;
            itemsScale[i] = items[i].localScale;
        }
       

    }
   
    void getTask()
    {
        List<ballTask> ballTasks = GameDataManager._instance.get_change_BallSkinTaskGameData();
        items = new Transform[ballTasks.Count];
        for (int i = 0; i < ballTasks.Count; i++)
        {
            UnityEngine.Object xx = Resources.Load("ui/taskPfb");
            GameObject tt = Instantiate(xx) as GameObject;
            items[i] = tt.transform;
            items[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("skin/"+ballTasks[i].skinSourceName);
            items[i].GetComponent<taskPfbControl>().taskInfo = ballTasks[i];
            items[i].parent = tasksParent;
        }
        setActive(new List<int> { items.Length / 2, items.Length / 2 - 1, items.Length / 2 + 1 });
        UpdateTaskInfoShow();
    }


  

    public void OnEndDrag(PointerEventData eventData)
    {


        if (scale > 0.5f || scale < -0.5f)//划过去了
        {
            if (scale > 0)//左
            {
                Transform[] tmp = new Transform[items.Length];

                for (int i = 0; i < items.Length; i++)
                {
                    if (i == 0)
                    {
                        items[i].position = itemsPosition[items.Length - 1];
                        items[i].localScale = itemsScale[items.Length - 1];
                    }
                    else
                    {
                        items[i].position = itemsPosition[i - 1];
                        items[i].localScale = itemsScale[i - 1];
                    }

                    if (i == items.Length - 1)
                    {
                        tmp[i] = items[0];
                    }
                    else
                    {
                        tmp[i] = items[i + 1];
                    }
                }
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = tmp[i];
                    itemsPosition[i] = items[i].position;
                    itemsScale[i] = items[i].localScale;
                }
                items[items.Length / 2 - 2].gameObject.SetActive(false);
                items[items.Length / 2 + 2].gameObject.SetActive(false);

            }
            else if (scale < 0)//右
            {
                Transform[] tmp = new Transform[items.Length];

                for (int i = 0; i < items.Length; i++)
                {

                    if (i == items.Length - 1)
                    {
                        items[i].position = itemsPosition[0];
                        items[i].localScale = itemsScale[0];
                    }
                    else
                    {
                        items[i].position = itemsPosition[i + 1];
                        items[i].localScale = itemsScale[i + 1];
                    }

                    if (i == 0)
                    {
                        tmp[i] = items[items.Length - 1];
                    }
                    else
                    {
                        tmp[i] = items[i - 1];
                    }
                }
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = tmp[i];
                    itemsPosition[i] = items[i].position;
                    itemsScale[i] = items[i].localScale;
                }
                items[items.Length / 2 - 2].gameObject.SetActive(false);
                items[items.Length / 2 + 2].gameObject.SetActive(false);
            }
        }
        else ///没化过去
		{
            Debug.Log(scale);
            for (int i = 0; i < items.Length; i++)
            {
                items[i].position = itemsPosition[i];
                items[i].localScale = itemsScale[i];
            }
            items[items.Length / 2 - 2].gameObject.SetActive(false);
            items[items.Length / 2 + 2].gameObject.SetActive(false);
        }
        UpdateTaskInfoShow();
        setActive(new List<int> { items.Length / 2, items.Length / 2 - 1, items.Length / 2 + 1 });
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x > ui_position_left.x && eventData.position.x < ui_position_right.x)
        {
            //scale = (ui_position_Midlle.x - eventData.position.x) / (ui_position_Midlle.x - ui_position_left.x);            
            scale = (ui_position_Midlle.x - eventData.position.x) / (ui_position_Midlle.x - ui_position_left.x);

            if (scale > 0)//在zuo侧区域滑动
            {
                setActive(new List<int> { items.Length / 2, items.Length / 2-1, items.Length / 2+1, items.Length / 2+2 });
                items[items.Length / 2].transform.position = new Vector3(itemsPosition[itemsPosition.Length / 2].x - (scale) * (itemsPosition[itemsPosition.Length / 2].x - itemsPosition[itemsPosition.Length / 2 - 1].x), ui_position_Midlle.y, 0);

                items[items.Length / 2].transform.localScale = new Vector3(1 - scale / 2, 1 - scale / 2, 1 - scale / 2);


                items[items.Length / 2 - 1].transform.localScale = new Vector3(0.5f - scale / 2, 0.5f - scale / 2, 0.5f - scale / 2);

                items[items.Length / 2 + 1].transform.localScale = new Vector3(0.5f + scale / 2, 0.5f + scale / 2, 0.5f + scale / 2);
                items[items.Length / 2 + 1].transform.position = new Vector3(itemsPosition[itemsPosition.Length / 2].x + (1 - scale) * (itemsPosition[itemsPosition.Length / 2].x - itemsPosition[itemsPosition.Length / 2 - 1].x), ui_position_Midlle.y, 0);

                items[items.Length / 2 + 2].transform.localScale = new Vector3(scale / 2, scale / 2, scale / 2);
                items[items.Length / 2 + 2].gameObject.SetActive(true);
            }
            else if (scale < 0)//在you侧区域滑动
            {
                setActive(new List<int> { items.Length / 2, items.Length / 2 - 1, items.Length / 2 + 1, items.Length / 2 - 2 });
                items[items.Length / 2].transform.position = new Vector3(itemsPosition[itemsPosition.Length / 2].x - (scale) * (itemsPosition[itemsPosition.Length / 2].x - itemsPosition[itemsPosition.Length / 2 - 1].x), ui_position_Midlle.y, 0);
                items[items.Length / 2].transform.localScale = new Vector3(1 + scale / 2, 1 + scale / 2, 1 + scale / 2);

                items[items.Length / 2 - 1].transform.localScale = new Vector3(0.5f - scale / 2, 0.5f - scale / 2, 0.5f - scale / 2);
                items[items.Length / 2 - 1].transform.position = new Vector3(itemsPosition[itemsPosition.Length / 2].x - (1 + scale) * (itemsPosition[itemsPosition.Length / 2].x - itemsPosition[itemsPosition.Length / 2 - 1].x), ui_position_Midlle.y, 0);

                items[items.Length / 2 + 1].transform.localScale = new Vector3(0.5f + scale / 2, 0.5f + scale / 2, 0.5f + scale / 2);

                items[items.Length / 2 - 2].transform.localScale = new Vector3(scale / 2, scale / 2, scale / 2);
                items[items.Length / 2 - 2].gameObject.SetActive(true);

            }
        }


  
    }

    void setActive(List<int> itemsIndex)
    {
        for (int i = 0; i < items.Length; i++)
        {
           
                if(itemsIndex.Contains(i))
                {
                    items[i].gameObject.SetActive(true);
                }
                else
                {
                    items[i].gameObject.SetActive(false);
                }
            
        }
    }
}