using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBallControl : MonoBehaviour
{
    public bool isMove;
    public int randomRotation = 90;
    public AIBallData ballData;
    Rigidbody _rigidbody;

    private gamedata gamedatas;


    private enum MonsterState
    {
        STAND,      //原地呼吸
        WALK,       //移动

    }
    private MonsterState currentState = MonsterState.STAND;          //默认状态为原地呼吸

    public float[] actionWeight = { 2000, 4000 };         //设置待机时各种动作的权重，顺序依次为呼吸、移动

    private float lastActTime;          //最近一次指令时间


    Vector3 fwd;

    private RaycastHit hit;
    private bool isTurn;
    private Vector3 targetRotation;//怪物的目标朝向
    private float walkSpeed;

    Vector3 initPosition;


    private void Awake()
    {
        isMove = true;
        initPosition = transform.position;
        _rigidbody = transform.GetComponent<Rigidbody>();

        gamedatas = DataManager._instance.Get(DataType._gamedata) as gamedata;

        walkSpeed = ((float)ballData.speed - (float)GameDataManager._instance.getBallSkillForLevel(gamedatas.aiBallReduceSpeedLevel).speedDown);
        DataManager._instance.AddDataWatch(DataType._gamedata, OnRefresh);


        //随机一个待机动作
        RandomAction();

    }
    /// <summary>
    /// 根据权重随机待机指令
    /// </summary>
    void RandomAction()
    {
        //更新行动时间
        lastActTime = Time.time;
        //根据权重随机
        float number = UnityEngine.Random.Range(0, actionWeight[0] + actionWeight[1]);
        if (number <= actionWeight[0])
        {
            currentState = MonsterState.STAND;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, UnityEngine.Random.Range(0, 360), transform.localEulerAngles.z);
        }
        else if (actionWeight[0] < number && number <= actionWeight[0] + actionWeight[1])
        {
            currentState = MonsterState.WALK;
            //随机一个朝向
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, UnityEngine.Random.Range(0,360), transform.localEulerAngles.z);

        }

    }


    private void OnRefresh(object[] param)
    {
        walkSpeed = ((float)ballData.speed - (float)GameDataManager._instance.getBallSkillForLevel(gamedatas.aiBallReduceSpeedLevel).speedDown);
    }


    bool istest = false;
    // Update is called once per frame
    void Update()
    {
        if(isMove == true)
        {
            fwd = transform.TransformDirection(new Vector3(0, -0.6f, 1f));
            if (Physics.Raycast(transform.position, fwd, out hit, 2f))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);

                if (hit.collider.gameObject.name != transform.parent.name)
                {

                    if (istest == false)
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 180, transform.localEulerAngles.z);
                        istest = true;
                    }


                }
                else
                {
                    istest = false;
                }

            }
            else
            {
                if (istest == false)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 180, transform.localEulerAngles.z);
                    istest = true;
                }

            }

            switch (currentState)
            {
                //待机状态，等待actRestTme后重新随机指令
                case MonsterState.STAND:
                    if (Time.time - lastActTime > 0.3f)
                    {
                        RandomAction();         //随机切换指令
                    }
                    break;


                //游走，根据状态随机时生成的目标位置修改朝向，并向前移动
                case MonsterState.WALK:

                    transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                    if (Time.time - lastActTime > 5)
                    {
                        RandomAction();         //随机切换指令
                    }

                    break;
            }
        }
        

    }


}
