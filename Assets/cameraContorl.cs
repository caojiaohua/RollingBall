using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraContorl : MonoBehaviour
{

     GameObject ball;
    public float cameraY = 8.25f;


    public float cameraZ = 8.3f;

    public void Start()
    {
        ball = GameObject.Find("ball");
    }
    void Update()
    {
         Camera.main.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + cameraY, ball.transform.position.z + cameraZ);
    }
}
