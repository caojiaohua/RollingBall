using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballContro : MonoBehaviour
{
    public float moveSpeed;

    Transform beforeGameoverPosition;
    private gamedata gamedatas;

    private void Start()
    {
        Debug.Log(gameObject.name);
        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;


        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);

        transform.GetComponent<Rigidbody>().mass = (float)GameDataManager._instance.getBallSkillForLevel(gamedatas.ballPowerLevel).power;

        moveSpeed = (float)GameDataManager._instance.getBallSkillForLevel(GameDataManager.getBallPowerLevel()).speed;

    }

    private void OnRefresh(object[] param)
    {
        transform.GetComponent<Rigidbody>().mass = (float)GameDataManager._instance.getBallSkillForLevel(gamedatas.ballPowerLevel).power;

        moveSpeed = (float)GameDataManager._instance.getBallSkillForLevel(gamedatas.ballPowerLevel).speed;
    }

    private void Update()
    {
        if (gamedatas.gameState == GAMESTATE.game)
            transform.Rotate(Vector3.right * moveSpeed, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "component")
        {
            componentControl xx = collision.gameObject.GetComponent<componentControl>();
            if (xx == null)
                xx = collision.transform.parent.GetComponent<componentControl>();

            //更新地图进度
            if (gamedatas.gameState == GAMESTATE.game && xx != null )
            {
                gamedatas.curGameProgressValue = ((float)xx.mapID / (float)gamedatas.mapComponentsNum) * 100;

                if (gamedatas.loadedComponentNum - xx.mapID <= 10)
                    gamedatas.MapRating += 2;
            }
            beforeGameoverPosition = collision.transform;
            gamedatas.Notify();
        }
        else if (collision.gameObject.tag == "aiball")
        {

        }
        else if(collision.gameObject.tag == "trap")
        {
            Debug.Log("gameover");
            gamedatas.gameState = GAMESTATE.over;
            UIPanelManager.Instance.PushPanel(UIPanelType.gameover);
            gamedatas.Notify();
        }
   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cube")
        {
            Debug.Log("gameover");
            gamedatas.gameState = GAMESTATE.over;
            UIPanelManager.Instance.PushPanel(UIPanelType.gameover);
            gamedatas.Notify();
        }
    }




    /// <summary>
    /// 复位 
    /// </summary>
    /// <param name="isPlace"> 是否原地复活</param>
    public void resetPosition(bool isPlace = false)
    {
        if(isPlace)
        transform.localPosition = new Vector3(0,0,0);
        else
        {
            transform.parent.position = new Vector3(0f,0.84f,-2f);
            transform.localPosition = new Vector3(0, 0, 0);

        }
    }
}
