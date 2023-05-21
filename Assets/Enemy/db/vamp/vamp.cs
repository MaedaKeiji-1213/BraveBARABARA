using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vamp : MonoBehaviour
{

    Vector2 target_pos;
    GameObject player;
    Animator anime;
    PartsParameter para;
    bool is_heal=false;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player");
        anime=GetComponent<Animator>();
        para=transform.parent.gameObject.GetComponent<PartsParameter>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(para.hp<=0)
            anime.CrossFadeInFixedTime("dead",0.3f);
        //else if()
    }
    void vampWarp ()
    {
        target_pos=player.transform.GetChild((int)Random.Range(0,player.transform.childCount-1)).position;
        transform.position=target_pos;
        transform.parent.localScale*=new Vector2(-1,1);
    }
}
