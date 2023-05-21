using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Dragon : MonoBehaviour
{
    [SerializeField] GameObject dragon_head;
    [SerializeField] GameObject dragon_breath;
    search_ray searchRay;
    GameObject player;
    Animator anime;
    Rigidbody2D rb;
    ushort motion;//1:fly 2:hipDrop 3:attack 4:tail 5:breath
    bool is_end_interval = true;
    bool on_breath = false;
    bool end_homing=true;

    [SerializeField]float fly_up_force ;
    [SerializeField]float fly_keep_force ;
    [SerializeField]float hipDrop_distance;
    [SerializeField]float breath_range;
    [SerializeField]float fly_height;
    [SerializeField]float fly_limitTime;
    float fly_leftTime=0;
    Vector3 old_scale;

    // Start is called before the first frame update
    void Start()
    {
        old_scale=transform.parent.localScale;
        player = GameObject.Find("head");
        anime = GetComponent<Animator>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        searchRay=GetComponent<search_ray>();
    }
    // Update is called once per frame
    void Update()
    {
        if(player==null)
            player = GameObject.Find("head");
        old_scale=transform.parent.localScale;
        //Debug.Log(anime.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //Debug.Log(anime.runtimeAnimatorController.animationClips[0].name);
        if (anime.GetCurrentAnimatorClipInfo(0)[0].clip.name != "dragon_breath")
        {
            on_breath = false;
        }
        if (on_breath)
        {
            HomingPlayer(player);
            Debug.Log("pla:"+player);
        }
        else if(end_homing)
        {
            HomingPlayer(0);
        }
        //dragon_breath.transform.localRotation=Quaternion.Euler(0,0,dragon_head.transform.lossyScale.x<0?150:30);

        if (is_end_interval &&(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1))
        {
            //Debug.Log("change"+anime.GetCurrentAnimatorStateInfo(0).normalizedTime);
            changeMotion();
            
        }
        
        Debug.DrawRay(transform.parent.position,Vector2.up*11,Color.black,0.1f);
        
        Debug.DrawRay(transform.parent.position,Vector2.down*30,Color.white,0.1f);
    }
    void changeMotion()
    {
        if (transform.position.x < player.transform.position.x )
            SetTransitionScale.Set(transform.parent.gameObject, new Vector3(-Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, 1), 0.5f);
        else if (transform.position.x > player.transform.position.x)
            SetTransitionScale.Set(transform.parent.gameObject, new Vector3(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, 1), 0.5f);
        bool near_high=false;
        bool near_row=false;
        bool rand=Random.Range(0,2f)<1?false:true;
        float range_distance = 20;
        float interval_distance =1;
        RaycastHit2D[] targets=searchRay.Search_sq(transform.parent.position+Vector3.down*(range_distance/2),
                                                    0,interval_distance,range_distance,15*-Mathf.Sign(transform.parent.localScale.x),6);
        int ray_value=(int)(range_distance/interval_distance)+1;
        for(int i=ray_value;i>=0;--i){
            if(targets[i]!=new RaycastHit2D()){
                if(i>ray_value/2)
                    near_high=true;
                else
                    near_row=true;
            }
        }

        if(motion==1&&Mathf.Abs(player.transform.position.x-transform.parent.position.x)<hipDrop_distance)
        {
            motion=2;
            playAnime("hip_drop", 0.2f);
        }
        else if(motion==1&&fly_leftTime>0)
        {
            fly_leftTime-=Time.deltaTime;
            playAnime("dragon_fly",0);
        }
        else if(near_high&&near_row)
        {
            if(rand)
                playAnime("dragon_attack", 0.2f);
            else
                playAnime("dragon_tail", 0.2f);
        }
        else if(near_high)
        {
            motion =3;
            playAnime("dragon_attack", 0.2f);
        }
        else if(near_row)
        {
            motion =4;
            playAnime("dragon_tail", 0.2f);
        }
        else if(motion!=1&&breath_range>=Mathf.Abs(player.transform.position.x-transform.parent.position.x))
        {
            motion=5;
            playAnime("dragon_breath", 0.2f);
        }
        else if(breath_range<Mathf.Abs(player.transform.position.x-transform.parent.position.x))
        {

            motion=1;
            playAnime("dragon_fly", 0.2f);
            fly_leftTime=fly_limitTime;
        }
    }
    void playAnime(string anime_name, float duration = 0)
    {
        //Debug.Log("a"+anime.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        //if(anime.GetCurrentAnimatorClipInfo(0)[0].clip.name==anime_name)
        //duration=0f;
        anime.CrossFadeInFixedTime(anime_name, duration);
        StartCoroutine("WaitAnimeCoroutine", duration);
    }
    IEnumerator WaitAnimeCoroutine(float duration)
    {
        is_end_interval = false;
        yield return new WaitForSeconds(duration);
        is_end_interval = true;
    }
    void HomingPlayer(GameObject target=null)
    {
        if(target!=null){
            if (transform.position.x < target.transform.position.x )
                SetTransitionScale.Set(transform.parent.gameObject, new Vector3(-Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, 1), 0.5f);
            else if (transform.position.x > target.transform.position.x)
                SetTransitionScale.Set(transform.parent.gameObject, new Vector3(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, 1), 0.5f);
            Vector3 dir = (target.transform.position - dragon_head.transform.position).normalized;
            Vector3 my_dir = Vector3.zero;
            float dragon_head_angle = dragon_head.transform.eulerAngles.z % 360 - (dragon_head.transform.eulerAngles.z % 360 <= 180 ? 0 : 360);
            dragon_head_angle+=transform.parent.localScale.x<0?-15:15;
            my_dir.x = Mathf.Cos((dragon_head_angle + 90) * Mathf.Deg2Rad);
            my_dir.y = Mathf.Sin((dragon_head_angle + 90) * Mathf.Deg2Rad);
            float rota_up_downA = (my_dir.x * dir.y - my_dir.y * dir.x);
            float rota_up_downB = 0;
            float min_angle = 20;
            float max_angle = 120;

            if (dragon_head.transform.lossyScale.x < 0)
            {
                rota_up_downB = -rota_up_downA;
                rota_up_downA = 0;
                min_angle = -140;
                max_angle = -40;
            }
            //Debug.Log("角度尾"+(dragon_head_angle%360));
            if ((dragon_head_angle >= min_angle && rota_up_downA < rota_up_downB)
                || (dragon_head_angle <= max_angle && rota_up_downA > rota_up_downB))
            {
                dragon_head.transform.Rotate(0, 0, (rota_up_downA + rota_up_downB) * 10);
            }
        }
    }
    void HomingPlayer(float target_angle)
    {   
        target_angle=target_angle % 360 - (target_angle % 360 <= 180 ? 0 : 360);
        float dragon_head_angle = dragon_head.transform.eulerAngles.z % 360 - (dragon_head.transform.eulerAngles.z % 360 <= 180 ? 0 : 360);
        dragon_head_angle+=transform.parent.localScale.x<0?-15:15;
        float min_angle = 20;
        float max_angle = 120;
        //Debug.Log("角度尾"+(dragon_head_angle%360));
        if ((dragon_head_angle >= min_angle && 0 < target_angle-dragon_head_angle)
            || (dragon_head_angle <= max_angle && 0 > target_angle-dragon_head_angle))
        {
            dragon_head.transform.Rotate(0, 0, Mathf.Sign(target_angle-dragon_head_angle) * 1);
        }
    }

    void Fly(int do_up)
    {
        RaycastHit2D up=new RaycastHit2D();
        up=Physics2D.Raycast(transform.parent.position,Vector2.up,11,~(1<<7));
        RaycastHit2D down=new RaycastHit2D();
        down=Physics2D.Raycast(transform.parent.position,Vector2.down,30,~(1<<7));
        if((up==new RaycastHit2D()&&down!=new RaycastHit2D())||do_up>0)
            rb.AddForce(new Vector2(0,fly_up_force));
        else 
            rb.AddForce(new Vector2(fly_up_force/2*-Mathf.Sign(transform.parent.localScale.x),fly_keep_force));
    }

    public void Reverse()
    {
        transform.localScale *= new Vector2(-1, 1);
    }
    public void OnBreath()
    {
        on_breath = true;
        end_homing=false;
    }
    public void OffBreath()
    {
        on_breath = false;
    }
    public void EndHoming()
    {
        end_homing=true;
    }
}
