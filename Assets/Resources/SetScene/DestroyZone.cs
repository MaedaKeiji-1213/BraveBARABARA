using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    [SerializeField]Transform parts_trans;
    string head_name="head";
    Vector3 head_pos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D col){
        //Debug.Log(col+"fa");
        if(col.transform.parent==parts_trans&&col.gameObject.name!=head_name){
            Destroy(col.gameObject);
        }
        if(col.gameObject.name==head_name){
            Quaternion head_rote=col.transform.rotation;
            col.transform.rotation=new Quaternion();
            col.transform.Translate(-(col.transform.position-head_pos)/20);
            col.transform.rotation=head_rote;
        }
    }
}
