using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private enum MonsterState
    {
        STAND,      //原地呼吸
        WALK,       //移动

    }
    private MonsterState currentState = MonsterState.STAND;          //默认状态为原地呼吸

    public float[] actionWeight = { 3000, 4000 };         //设置待机时各种动作的权重，顺序依次为呼吸、移动

    private float lastActTime;          //最近一次指令时间


    Vector3 fwd;

    private RaycastHit hit;
    private bool isTurn;
    private Quaternion targetRotation;//怪物的目标朝向
    private float walkSpeed;

    Vector3 initPosition;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        walkSpeed = 2.0f;
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
        initPosition = new Vector3(Random.Range(0, 5),0.75f, Random.Range(0, 5));
        //根据权重随机
        float number = Random.Range(0, actionWeight[0] + actionWeight[1]);
        if (number <= actionWeight[0])
        {
            currentState = MonsterState.STAND;
        }
        else if (actionWeight[0] < number && number <= actionWeight[0] + actionWeight[1])
        {
            currentState = MonsterState.WALK;
            //随机一个朝向
            targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        fwd = transform.TransformDirection(new Vector3(0, -0.3f, 1f));
        if (Physics.Raycast(transform.position, fwd, out hit, 2)   )
        {
            isTurn = false;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            isTurn = true;
            
            
        }

        switch (currentState)
        {
            //待机状态，等待actRestTme后重新随机指令
            case MonsterState.STAND:
                if (Time.time - lastActTime > 0.8f)
                {
                    RandomAction();         //随机切换指令
                }
                break;


            //游走，根据状态随机时生成的目标位置修改朝向，并向前移动
            case MonsterState.WALK:
                if(isTurn == false)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
                }
   

                if (Time.time - lastActTime > 8)
                {
                    RandomAction();         //随机切换指令
                }
                //该状态下的检测指令
                if(isTurn == true)
                {
                    targetRotation = Quaternion.LookRotation(initPosition - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
                }
                break;
        }

    }
}
