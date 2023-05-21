using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
public class KnockBack : MonoBehaviour
{
    public  float[] knockback_rate;
    int knockback_hp=0;
    [SerializeField,Tooltip("KnockBackForce")]Vector2 knockback_force=Vector3.zero;
    GameObject shockwave;
    PartsParameter pp;
    Rigidbody2D rb2D;
    int i=0;
    // Start is called before the first frame update
    void Start()
    {
        shockwave=Resources.Load<GameObject>("Shockwave");
        pp=GetComponent<PartsParameter>();
        rb2D=GetComponent<Rigidbody2D>();
        if(pp!=null&&knockback_rate.Length>0)
        {
            knockback_rate=knockback_rate.OrderBy(a=>-a).ToArray<float>();
            knockback_hp=(int)(knockback_rate[i]*pp.max_hp);
        }
        //StartCoroutine(SripDamage());
    }

    // Update is called once per frame
    void Update()
    {
        if(knockback_hp<0&&knockback_hp>=pp.hp)
        { 
            Debug.Log("Knock"+knockback_hp);
            if(rb2D!=null){
                rb2D.AddForce(new Vector2(knockback_force.x*Math.Sign(transform.parent.localScale.x),knockback_force.y));
                Destroy(Instantiate(shockwave,transform.position,Quaternion.identity),2);
            }
            if(i+1<knockback_rate.Length){
                knockback_hp=(int)(knockback_rate[++i]*pp.max_hp);
            }
            else
                knockback_hp=-1;
        }
    }
    // IEnumerator SripDamage()
    // {
    //     while(true){
    //         yield return new WaitForSeconds(0.5f);
    //         pp.hp--;
    //     }
    // }
}
