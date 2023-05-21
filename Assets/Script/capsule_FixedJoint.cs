using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capsule_FixedJoint : MonoBehaviour
{
    float time_destroy=0;
    Vector2 vec=Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D my_col=GetComponent<CapsuleCollider2D>();
        float ofs_angle=((Mathf.Atan2(my_col.offset.y,my_col.offset.x)*Mathf.Rad2Deg)+transform.rotation.eulerAngles.z);
        vec.x=Mathf.Cos(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.x);
        vec.y=Mathf.Sin(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.y);
        //vec=my_col.offset*transform.localScale;
        //Debug.Log(transform.name+":"+vec);

        Collider2D[] colliders=Physics2D.OverlapCapsuleAll((Vector2)transform.position+vec,GetComponent<CapsuleCollider2D>().size*transform.lossyScale,GetComponent<CapsuleCollider2D>().direction,transform.rotation.z);
        foreach(Collider2D col in colliders){
            if(transform.root==col.transform.root&&col.gameObject.tag=="Player"&&col!=my_col&&col.isTrigger==false&&col.gameObject.layer!=3){
                gameObject.AddComponent<FixedJoint2D>().connectedBody=col.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        //Instantiate((GameObject)Resources.Load("Circle"),(Vector2)transform.position+vec,Quaternion.Euler(0,0,ofs_angle-90),transform).name=transform.name;
        Destroy(GetComponent<capsule_FixedJoint>());
        //Instantiate((GameObject)Resources.Load("Circle"),(Vector2)transform.position+vec,Quaternion.Euler(0,0,ofs_angle-90)).name=transform.name;
        //Debug.Log("Atan"+((Mathf.Atan2(my_col.offset.y,my_col.offset.x)*Mathf.Rad2Deg)+transform.rotation.eulerAngles.z));
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
