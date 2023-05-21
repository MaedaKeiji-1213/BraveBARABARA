using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotenController : MonoBehaviour
{
    Animator anime;
    GameObject player;
    [SerializeField] float erea;
    [SerializeField] float cool_time;
    PartsParameter pp;
    bool enable_attack;
    // Start is called before the first frame update
    void Start()
    {
        anime = transform.GetChild(0).GetComponent<Animator>();
        player = GameObject.Find("head");
        pp=GetComponent<PartsParameter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
            player = GameObject.Find("head");
        if (anime != null)
        {
            if(pp.hp<=0){
                anime.CrossFadeInFixedTime("SabotenDead", 0.5f);
                Destroy(GetComponent<SabotenController>());
            }
            else if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
                (player.transform.position - transform.position).magnitude < erea)
            {
                anime.CrossFadeInFixedTime("Saboten", 0);
            }
        }
    }
    IEnumerator Cooltime(float _time)
    {

        yield return new WaitForSeconds(_time);
    }
}
