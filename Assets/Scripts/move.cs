using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float moveSpeed = 2f;
    float angle_copy = 999;
    public void onJoystickValueChanged(Vector2 eventPointerData)
    {
#if UNITY_IOS || UNITY_ANDROID
        float angle = Vector2.SignedAngle(new Vector2(1, 0), eventPointerData);
        
        
        if (angle != 0)
        {
            if (angle < 22.5f && angle > -22.5f)
            {
                //右
                RotateMove(new Vector3(0, -90, 0));
            }
            else if (angle < -22.5f && angle > -67.5f)
            {
                //右下
                RotateMove(new Vector3(0, -45, 0));
            }
            else if (angle < -67.5f && angle > -112.5f)
            {
                //下
                RotateMove(new Vector3(0, 0, 0));
            }
            else if (angle < -112.5f && angle > -157.5f)
            {
                //左下
                RotateMove(new Vector3(0, 45, 0));
            }
            else if (angle < -157.5f || angle > 157.5f)
            {
                //左
                RotateMove(new Vector3(0, 90, 0));
            }
            else if (angle < 157.5f && angle > 112.5f)
            {
                //左上
                RotateMove(new Vector3(0, 135, 0));
            }
            else if (angle < 112.5f && angle > 67.5f)
            {
                //上
                RotateMove(new Vector3(0, 180, 0));
            }
            else if (angle < 67.5f && angle > 22.5f)
            {
                //右上
                RotateMove(new Vector3(0, -135, 0));
            }
            angle_copy = angle;
        }
        else if(angle_copy != 999)
        {
            if (angle_copy < 22.5f && angle_copy > -22.5f)
            {
                //右
                RotateMove(new Vector3(0, -90, 0));
            }
            else if (angle_copy < -22.5f && angle_copy > -67.5f)
            {
                //右下
                RotateMove(new Vector3(0, -45, 0));
            }
            else if (angle_copy < -67.5f && angle_copy > -112.5f)
            {
                //下
                RotateMove(new Vector3(0, 0, 0));
            }
            else if (angle_copy < -112.5f && angle_copy > -157.5f)
            {
                //左下
                RotateMove(new Vector3(0, 45, 0));
            }
            else if (angle_copy < -157.5f || angle_copy > 157.5f)
            {
                //左
                RotateMove(new Vector3(0, 90, 0));
            }
            else if (angle_copy < 157.5f && angle_copy > 112.5f)
            {
                //左上
                RotateMove(new Vector3(0, 135, 0));
            }
            else if (angle_copy < 112.5f && angle_copy > 67.5f)
            {
                //上
                RotateMove(new Vector3(0, 180, 0));
            }
            else if (angle_copy < 67.5f && angle_copy > 22.5f)
            {
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
