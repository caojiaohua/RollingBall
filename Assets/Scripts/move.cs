using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float moveSpeed = 2f;

    public float cameraY = 19.36f;


    public float cameraZ = 12.4f;

    void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + cameraY, transform.position.z + cameraZ);
    }
    public void onJoystickValueChanged(Vector2 eventPointerData)
    {
#if UNITY_IOS || UNITY_ANDROID
        float angle = Vector2.SignedAngle(new Vector2(1, 0), eventPointerData);
        //Debug.Log(angle);

        if (angle != 0)
        {
            //  RotateMove(new Vector3(0, -(angle + 67.5f), 0));

            if (angle < 22.5f && angle > -22.5f)
            {
                //  Debug.Log("右");
                //右
                RotateMove(new Vector3(0, -90, 0));
            }
            else if (angle < -22.5f && angle > -67.5f)
            {
                //  Debug.Log("右下");
                //右下
                RotateMove(new Vector3(0, -45, 0));
            }
            else if (angle < -67.5f && angle > -112.5f)
            {
                //   Debug.Log("下");
                //下
                RotateMove(new Vector3(0, 0, 0));
            }
            else if (angle < -112.5f && angle > -157.5f)
            {
                //  Debug.Log("左下");
                //左下
                RotateMove(new Vector3(0, 45, 0));
            }
            else if (angle < -157.5f || angle > 157.5f)
            {
                //  Debug.Log("左");
                //左
                RotateMove(new Vector3(0, 90, 0));
            }
            else if (angle < 157.5f && angle > 112.5f)
            {
                //   Debug.Log("左上");
                //左上
                RotateMove(new Vector3(0, 135, 0));
            }
            else if (angle < 112.5f && angle > 67.5f)
            {
                //  Debug.Log("上");
                //上
                RotateMove(new Vector3(0, 180, 0));
            }
            else if (angle < 67.5f && angle > 22.5f)
            {
                //  Debug.Log("右上");
                //右上
                RotateMove(new Vector3(0, -135, 0));
            }
        }

#endif
    }

    private void RotateMove(Vector3 vet3)
    {


        
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);

        
        transform.eulerAngles = vet3;
    }


}
