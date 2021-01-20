using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroControl : MonoBehaviour
{
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);
        boxes = new List<Transform>();

    }
    List<Transform> boxes;

    private void OnRefresh(object[] param)
    {

        transform.GetComponent<MeshRenderer>().material = GameDataManager.getBallSkinForSkinId(gamedatas.curChooseBallSkinId);
        if (gamedatas.isTest == true)
        {
            gamedatas.mapComponentsNum = GameDataManager._instance.getTestMapComponentNum();
        }
        else if (gamedatas.isTest == false)
        {
            gamedatas.mapComponentsNum = GameDataManager._instance.getMapComponentNum();
        }
    }
    private gamedata gamedatas;
    private Transform beforeGameoverPosition;
    private RaycastHit hit;
    private float lastActTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "box")
        {
            collision.transform.parent = transform;

            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1 + index * 0.1f, transform.position.z);
            collision.transform.localEulerAngles = new Vector3(0, 0, 0);
            index += 1;
            boxes.Add(collision.transform);
        }
        if (collision.gameObject.tag == "component")
        {
            componentControl xx = collision.gameObject.GetComponent<componentControl>();
            if (xx == null)
                xx = collision.transform.parent.GetComponent<componentControl>();

            //更新地图进度
            if (gamedatas.gameState == GAMESTATE.game && xx != null)
            {
                if (((float)xx.mapID / (float)gamedatas.mapComponentsNum) * 100 > gamedatas.curGameProgressValue)
                {
                    transform.parent.GetComponent<move>().isMove = true;
                    gamedatas.curGameProgressValue = ((float)xx.mapID / (float)gamedatas.mapComponentsNum) * 100;
                    gamedatas.GameProgressValue = ((float)xx.mapID / (float)gamedatas.mapComponentsNum) * 100;

                    gamedatas.curGameGoldValue += (int)(GameDataManager._instance.getGoldUpgradeForLevel(gamedatas.GoldMulitipleLevel).distanceIncome * 2.5f);
                    gamedatas.GameGoldValue += (int)(GameDataManager._instance.getGoldUpgradeForLevel(gamedatas.GoldMulitipleLevel).distanceIncome * 2.5f);
                }
            }
            if (gamedatas.loadedComponentNum - xx.mapID <= 10)
            {

                gamedatas.MapRating += 1;
                gamedatas.Notify();
                GameControl._instance.setGameMap();

            }
            if (collision.transform.GetComponent<componentControl>() == null)
                beforeGameoverPosition = collision.transform.parent;
            else
                beforeGameoverPosition = collision.transform;
            gamedatas.beforeGameOverMapID = xx.mapID;




            gamedatas.Notify();
        }
        

    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gameover")
        {
            if (gamedatas.gameState != GAMESTATE.over)
            {

                Debug.Log("gameover");
                gamedatas.gameState = GAMESTATE.over;
                UIPanelManager.Instance.PushPanel(UIPanelType.gameover);
                gamedatas.Notify();
            }

        }
    }

    /// <summary>
    /// 复位 
    /// </summary>
    /// <param name="isPlace"> 是否原地复活</param>
    public void resetPosition(bool isPlace = false)
    {
        if (isPlace)
        {
            if (beforeGameoverPosition != null)
            {
                Vector3 gameoverPoint_startPoint = new Vector3();
                foreach (var item in beforeGameoverPosition.GetComponentsInChildren<Transform>())
                {
                    if (item.gameObject.tag == "startPoint")
                        gameoverPoint_startPoint = item.transform.position;
                }
                transform.parent.position = new Vector3(gameoverPoint_startPoint.x, 0.84f, gameoverPoint_startPoint.z);
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.parent.position = new Vector3(0f, 0.84f, -2f);
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            transform.parent.position = new Vector3(0f, 0.84f, -2f);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localEulerAngles = new Vector3(0, 0, 0);

        }
    }


    
    
}
