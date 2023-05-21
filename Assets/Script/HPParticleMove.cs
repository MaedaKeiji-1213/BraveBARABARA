using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPParticleMove : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 vec;
    Rigidbody2D rb;
    Collider2D my_col;
    bool is_touched=true;
    [SerializeField,Tooltip("force")] Vector2 force=Vector2.zero;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.AddForce(force);
        my_col=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        is_touched=false;
        if(is_touched){
            float ofs_angle=((Mathf.Atan2(my_col.offset.y,my_col.offset.x)*Mathf.Rad2Deg)+transform.rotation.eulerAngles.z);
            vec.x=Mathf.Cos(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.x);
            vec.y=Mathf.Sin(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.y);
            Collider2D[] colliders=Physics2D.OverlapBoxAll((Vector2)transform.position+vec,GetComponent<BoxCollider2D>().size*transform.lossyScale,transform.rotation.z);
            foreach(Collider2D col in colliders){
                if(col.gameObject.layer!=3&&col!=my_col&&col.isTrigger==false){
                    gameObject.AddComponent<FixedJoint2D>().connectedBody=col.gameObject.GetComponent<Rigidbody2D>();
                    is_touched=true;
                }
            }
        }
        else if(rb.velocity.y<0){
            gameObject.layer=0;
        }
        
    }
    
}
