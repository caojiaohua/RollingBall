using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBallControl : MonoBehaviour
{
    public AIBallData ballData;
    Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        rigidbody.mass = (float)ballData.power;
        
    }
}
