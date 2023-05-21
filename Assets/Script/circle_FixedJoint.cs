using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_FixedJoint : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 vec=Vector2.zero;
    void Start()
    {
        Collider2D my_col=GetComponent<CircleCollider2D>();
        float ofs_angle=((Mathf.Atan2(my_col.offset.y,my_col.offset.x)*Mathf.Rad2Deg)+transform.rotation.eulerAngles.z);
        vec.x=Mathf.Cos(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.x);
        vec.y=Mathf.Sin(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.y);

        Collider2D[] colliders=Physics2D.OverlapCircleAll((Vector2)transform.position+vec,GetComponent<CircleCollider2D>().radius*transform.lossyScale.x);
        foreach(Collider2D col in colliders){
            if(transform.root==col.transform.root&&col.gameObject.tag=="Player"&&col!=my_col&&col.isTrigger==false&&col.transform!=transform.parent&&col.gameObject.layer!=3){
                gameObject.AddComponent<FixedJoint2D>().connectedBody=col.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        Debug.Log(transform.lossyScale);
        Destroy(GetComponent<circle_FixedJoint>());
        Instantiate((GameObject)Resources.Load("Circle"),(Vector2)transform.position+vec,Quaternion.Euler(0,0,ofs_angle-90),transform).name=transform.name;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
