using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ElevatorController : MonoBehaviour
{
    [SerializeField] float speed_x;
    [SerializeField] float speed_y;
    [SerializeField] float moving_distance;
    Vector3 origin_position;
    Vector3 end_position;
    [SerializeField] float stand_time;
    bool enable_move=true;
    Rigidbody2D rb;
    public bool is_turn=false;
    
    [SerializeField] float force_x;
    [SerializeField] float force_y;
    bool is_up=false; 
    bool is_down=false;
    List<GameObject> objs=new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        Vector2 speed_dir = new Vector2(speed_x, speed_y);
        origin_position = transform.position;
        speed_dir /= speed_dir.magnitude;

        end_position = (Vector2)transform.position + speed_dir * moving_distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enable_move){
            if ((origin_position.x >= transform.position.x &&
                origin_position.y >= transform.position.y&&!is_up) ||
                (end_position.x <= transform.position.x &&
                end_position.y <= transform.position.y&&!is_down))
            {
                speed_x *= -1;
                speed_y *= -1;
                is_turn=true;
                StartCoroutine(Wait(stand_time));
                if(is_up)
                {
                    is_up=false;
                    is_down=true;
                }
                else 
                {
                    is_up=true;
                    is_down=false;
                }
            }            
            rb.velocity=(new Vector3(-speed_x, -speed_y, 0));
        }
        else
            rb.velocity=Vector2.zero;
        

        Vector2 vec=Vector2.zero;
        Collider2D my_col=GetComponent<BoxCollider2D>();
        float ofs_angle=((Mathf.Atan2(my_col.offset.y,my_col.offset.x)*Mathf.Rad2Deg)+transform.rotation.eulerAngles.z);
        vec.x=Mathf.Cos(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.x);
        vec.y=Mathf.Sin(ofs_angle*Mathf.Deg2Rad)*(my_col.offset.magnitude*transform.lossyScale.y);
        Collider2D[] colliders=Physics2D.OverlapBoxAll((Vector2)transform.position+vec,GetComponent<BoxCollider2D>().size*transform.lossyScale,transform.rotation.z);
        foreach(Collider2D col in colliders){
            if(col.gameObject.layer==6&&col.isTrigger==false&&col.gameObject.layer!=3){
                StartCoroutine(Untouch(col.gameObject,0.3f));
            }
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {   
        Rigidbody2D rb=col.gameObject.GetComponent<Rigidbody2D>();
        if(rb!=null&&is_turn){
            Vector2 sign=Vector2.zero;
            sign.x=Mathf.Sign(col.transform.position.x-transform.position.x);
            sign.y=Mathf.Sign(col.transform.position.y-transform.position.y);
            rb.AddForce(new Vector3(Mathf.Abs(speed_x)*force_x*rb.mass*sign.x,Mathf.Abs(speed_y)*force_y*rb.mass*sign.y, 0));
        }
    }
    IEnumerator Wait(float _time)
    {
        enable_move=false;
        yield return new WaitForSeconds(_time);
        enable_move=true;
    }
    IEnumerator Untouch(GameObject player_obj,float _time)
    {
        gameObject.layer=6;
        yield return new WaitForSeconds(_time);
        gameObject.layer=8;
    }
}
    
