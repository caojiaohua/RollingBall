using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    int[] mapsV ;
    
    Vector3 nowPosition;
    // Start is called before the first frame update
    void Start()
    {
        mapsV = new int[4] { 1,2,3,4};
        nowPosition = new Vector3(0,0,0);
        init();
    }
    public Transform mapsParent;

    void init()
    {
        for(int i = 0;i< mapsV.Length; i++)
        {
            GameObject map = Instantiate(Resources.Load(mapsV[i].ToString())) as GameObject;
          
            nowPosition += new Vector3(0,0, (float)System.Math.Round(map.transform.GetComponent<Renderer>().bounds.size.z / 1, 2));

            map.transform.localPosition = new Vector3(nowPosition.x, nowPosition.y, nowPosition.z ); 
        }
    }

}
