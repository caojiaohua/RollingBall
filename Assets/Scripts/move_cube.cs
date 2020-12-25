using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_cube : MonoBehaviour
{
    GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("ball");
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.transform.position.y>=0.8f)
        transform.position = new Vector3(ball.transform.position.x,ball.transform.position.y -2,ball.transform.position.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AIBall")
        {
            ///更新金币
            ///

          //  GameDataManager._instance.setGoldNum((int)(GameDataManager._instance.getGoldUpgradeForLevel(GameDataManager._instance.getGoldMultipleLevel()).killEnemyIncome));

            ///更新杀敌数
            ///
            GameDataManager._instance.setKillAINum();
            JoystickPanel._instance.setKillAINum();

        }
        else if(other.tag == "ball")
        {
            Debug.Log("game over");
        }
    }
}
