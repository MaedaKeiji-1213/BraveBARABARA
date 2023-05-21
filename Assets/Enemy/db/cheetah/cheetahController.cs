using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheetahController : MonoBehaviour
{

    [SerializeField]AnimationCurve jump_curve; 
    [SerializeField]float jump_force;
    [SerializeField]float dush_force;
    [SerializeField]float dush_time;
    float left_time=0;
    [SerializeField]float max_velocity;
    [SerializeField]GameObject eye;
    Transform player;
    search_ray sr;
    Rigidbody2D rb;
    Animator anime;
    PartsParameter para;
    ushort motion;//1:chage 2:dash 3:jump 
    float interval_angle=0.25f;
    float range_angle=89;
    float length=40;
    bool is_end_interval=true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        para=transform.parent.GetComponent<PartsParameter>();
        anime=GetComponent<Animator>();
        rb=transform.parent.GetComponent<Rigidbody2D>();
        sr=GetComponent<search_ray>();
        player=GameObject.Find("head").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player==null)
            player=GameObject.Find("head").transform;
        if(para.hp<=0){
            playAnime("dead",0.5f);
            Destroy(GetComponent<cheetahController>());
        }
        if(left_time>0)left_time-=Time.deltaTime;
        //Debug.Log("time"+anime.GetCurrentAnimatorStateInfo(0).normalizedTime+":"+is_end_interval);
        if(is_end_interval&&
            (anime.GetCurrentAnimatorStateInfo(0).normalizedTime>=1||(motion==2&&-rb.velocity.x*transform.lossyScale.x<=0))){
            //Debug.Log("change"+anime.GetCurrentAnimatorStateInfo(0).normalizedTime);
            changeMotion();
            transform.localScale=new Vector2(Mathf.Abs(transform.localScale.x)*-Mathf.Sign(player.position.x-transform.position.x),transform.localScale.y);

        }
        
        if(anime.speed==0&&rb.velocity.y<=0)
            Move_anime();
        sr.Search(eye.transform.position,((transform.lossyScale.x>0)?135:45),interval_angle,range_angle,length,~(1 << 7));
    }
    void FixedUpdate(){
        
        if(motion==2)
            Dash();
    }

    void changeMotion()
    {   

        if(motion==2&&anime.GetCurrentAnimatorStateInfo(0).normalizedTime<1){
            motion=3;
            playAnime("jump",0.2f);
            //Debug.Log("3");
        }
        else if(motion==1){
            left_time=dush_time;
            motion=2;
            playAnime("dash",0.2f);
            //Debug.Log("2");
        }
        else if(motion==2&&left_time>0){
            motion=2;
            playAnime("dash");
            //Debug.Log("2.5");
        }
        else{
            motion=1;
            playAnime("charge",0.2f);
            //Debug.Log("1");
        }
    }
    void playAnime(string anime_name,float duration=0)
    {
        //Debug.Log("a"+anime.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        //if(anime.GetCurrentAnimatorClipInfo(0)[0].clip.name==anime_name)
            //duration=0f;
        anime.CrossFadeInFixedTime(anime_name,duration);
        StartCoroutine("WaitAnimeCoroutine",duration);
    }
    IEnumerator WaitAnimeCoroutine(float duration){
        is_end_interval=false;
        yield return new WaitForSeconds(duration);
        is_end_interval=true;
    }
    public void Jump()
    {
        float search_angle=transform.rotation.eulerAngles.z+((transform.lossyScale.x>0)?135:45);
        int ray_value=(int)(range_angle/interval_angle)+1;
        RaycastHit2D[] targets=new RaycastHit2D[ray_value];
        targets=sr.Search(eye.transform.position,search_angle,interval_angle,range_angle,length,~(1 << 7));
        Vector2 jump_force_result=Vector2.zero;
        Vector2 dif=Vector2.zero;
        for(int i=ray_value;i>=0;--i){
            if(targets[i]!=new RaycastHit2D()){
                if(dif.magnitude<((Vector2)eye.transform.position-targets[i].point).magnitude/10)
                    dif=(targets[i].point-(Vector2)eye.transform.position)/10;
                //dif=vec_swap(dif);
                dif.x=Mathf.Abs(dif.x)*-Mathf.Sign(transform.lossyScale.x);
                jump_force_result=dif*jump_force*jump_curve.Evaluate(dif.magnitude/length);//Debug.Log("jump:"+dif.magnitude);
            }
        }
        rb.AddForce(jump_force_result);//Debug.Log("jfr"+jump_force_result.magnitude);//*-Mathf.Sign(transform.lossyScale.x));
        //Debug.Log("jump:"+jump_force);
        
    }

    void Dash(){
        Vector2 use_force=Vector2.zero;
        use_force.x=dush_force*Mathf.Sign(-transform.lossyScale.x);//*(1-rb.velocity.x/max_velocity);
        rb.AddForce(use_force);
    }
    public void Stop_anime(){
        anime.speed=0;
        rb.AddForce(Vector2.right*250*-Mathf.Sign(transform.lossyScale.x));   
    }
    void Move_anime(){
        anime.speed=1;
        rb.AddForce(Vector2.right*500*-Mathf.Sign(transform.lossyScale.x));     
    }
    Vector2 vec_swap(Vector2 vec){
        float tmp=vec.x;
        vec.x=vec.y;
        vec.y=tmp;
        return vec;
    }
}
