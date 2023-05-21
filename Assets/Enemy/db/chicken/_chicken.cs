using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _chicken : MonoBehaviour
{
    //Vector3 movingDirecion;
    [SerializeField]float speed_mag;
    [SerializeField]float max_vel;
    [SerializeField]float return_interval;
    float walk_time=0;
    private Rigidbody2D rb;
    
    search_ray sr;
    [SerializeField]GameObject head_obj;
    CircleCollider2D head;

    [SerializeField]int dir_sign=1;
    Animator animator;
    PartsParameter pp;

    // Start is called before the first frame update
    void Start()
    {
        pp=GetComponent<PartsParameter>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        sr=GetComponent<search_ray>();
        //head_obj=transform.Find("head").gameObject;
        Debug.Log("head_obj<-"+head_obj);
        head=head_obj.GetComponent<CircleCollider2D>();
        
        rb.AddForce(Vector2.right*speed_mag, ForceMode2D.Force);
        animator=GetComponentInChildren<Animator>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        RaycastHit2D player=new RaycastHit2D();
        //Debug.Log(player);
        Vector3 head_pos=head_obj.transform.position+(Vector3)(head.offset*head_obj.transform.lossyScale);
        Vector3 dir=Vector3.zero;
        float search_angle=transform.rotation.eulerAngles.z+((transform.GetChild(0).lossyScale.x>0)?-30:210);
        RaycastHit2D[] targets=sr.Search(head_pos,search_angle,5,90,15,(1 << 6));
        foreach(RaycastHit2D target in targets){
            if(target.transform!=new RaycastHit2D()&&(player==new RaycastHit2D()||target.fraction<player.fraction)){
                player=target;
            }
        }
        if(pp.hp<=0)
        {
            //animator.CrossFadeInFixedTime("chicken_dead",0.5f);
            animator.SetInteger("trans",3);
        }
        else if(player!=new RaycastHit2D()&&player.fraction<0.25f){//Debug.Log(player.fraction);
            animator.SetInteger("trans",2);
        }
        else if(player!=new RaycastHit2D()){//Debug.Log("is_player");
            animator.SetInteger("trans",1);
            dir.x=Mathf.Sign(player.transform.position.x-transform.position.x)*speed_mag*2;
            walk_time=return_interval/2;
        }
        else{//Debug.Log("isn't_player");
            animator.SetInteger("trans",1);
            if(walk_time>=return_interval){
                dir_sign*=-1;
                walk_time=0;
            }
            else{
                walk_time+=Time.deltaTime;
            }
            dir.x=dir_sign*speed_mag;
        }
        rb.AddForce(dir*(1-rb.velocity.x/max_vel), ForceMode2D.Force);
        transform.GetChild(0).localScale=new Vector2(Mathf.Abs(transform.GetChild(0).localScale.x)*dir_sign,transform.GetChild(0).localScale.y); 
    }
}
