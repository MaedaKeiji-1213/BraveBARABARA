using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saboten : MonoBehaviour
{
    [SerializeField] int damage;
    GameObject hp_particle;
    bool enable_attack=true;
    float cool_time=0.25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hp_particle=Resources.Load<GameObject>("HPParticle");
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (gameObject.tag !=col.gameObject.tag&&enable_attack)
        {
            PartsParameter pp = col.gameObject.GetComponent<PartsParameter>();
            if (pp != null&&pp.hp>0) {
                pp.hp -= damage;
                GameObject made_hp=Instantiate(hp_particle,col.GetContact(0).point,Quaternion.identity);
                TextMesh hp_text=made_hp.GetComponentInChildren<TextMesh>();
                hp_text.text=(-damage).ToString();
                if(col.transform.tag=="Player")
                    hp_text.color=Color.red;
                else
                    hp_text.color=Color.green;
                StartCoroutine(AttackCoolTime(cool_time));
            }
        }
    }
    IEnumerator AttackCoolTime(float _time=0)
    {
        enable_attack=false;
        yield return new WaitForSeconds(_time);
        enable_attack=true;
    }
}
