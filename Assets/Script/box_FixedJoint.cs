using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_FixedJoint : MonoBehaviour
{
    float time_destroy=0;
    Vector2 vec;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D my_col=GetComponent<BoxCollider2D>();
        float ofs_angle=((Mathf.Atan2(my_col.offset.y,my_col.offset.x)*Mathf.Rad2Deg)+transform.rotation.eulerAngles.z);
        vec.x=Mathf.Cos(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.x);
        vec.y=Mathf.Sin(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.y);
        Collider2D[] colliders=Physics2D.OverlapBoxAll((Vector2)transform.position+vec,GetComponent<BoxCollider2D>().size*transform.lossyScale,transform.rotation.z);
        foreach(Collider2D col in colliders){
            if(transform.root==col.transform.root&&col.gameObject.tag=="Player"&&col.gameObject.layer==6&&col!=my_col&&col.isTrigger==false){
                gameObject.AddComponent<FixedJoint2D>().connectedBody=col.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        Destroy(GetComponent<box_FixedJoint>());
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*void OnCollisionStay2D( Collision2D col){
        if(col.gameObject.tag=="Player"){
        }
    }*/
}
