using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    [SerializeField]float regist_cool_time;
    [SerializeField]float regist_power;
    bool enable_regist=true;
    List<Rigidbody2D> rbs=new List<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enable_regist)
        {
            if(rbs!=default){
                //Debug.Log("rb=0");
                foreach (Rigidbody2D rb in rbs)
                {
                    rb.velocity=rb.velocity*regist_power;
                    rb.angularVelocity=rb.angularVelocity*regist_power;
                }
            }
            rbs=new List<Rigidbody2D>();
            StartCoroutine(RegistCoolTime(regist_cool_time));
        }
    }
    IEnumerator RegistCoolTime(float cooltime)
    {
        enable_regist=false;
        yield return new WaitForSeconds(cooltime);
        enable_regist=true;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        Rigidbody2D rb= col.gameObject.GetComponent<Rigidbody2D>();
        if(rb!=null){
            Debug.Log("rbさん");
            rbs.Add(rb);
        }
    }
}
