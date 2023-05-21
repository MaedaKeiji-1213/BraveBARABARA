using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.parent.parent.name);
        Transform grand=transform.parent.parent;
        //transform.localScale=new Vector2(transform.localScale.x/grand.localScale.x,transform.localScale.y/grand.localScale.y);
        transform.rotation=Quaternion.Euler(0,0,-grand.transform.rotation.z);
        //transform.rotation=new Quaternion(0,0,0,0);
    }
}
