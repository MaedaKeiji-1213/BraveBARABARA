using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battle_damage : MonoBehaviour
{
    ParticleSystem particle_system;
    public List<ParticleCollisionEvent> collisionEvents;
    Rigidbody2D rb;
    PartsParameter parameter;
    [System.NonSerialized] public float defence;
    [System.NonSerialized] public float attack;
    [System.NonSerialized] public float vel;
    [System.NonSerialized] public int damage=0;
    [SerializeField] float vel_sub;
    float hp;
    public float attack_cool_time=0.5f;
    public float damage_cool_time=1;
    public float damage_cut;
    GameObject hp_particle;

    bool enable_damage=true;
    bool enable_attack=true;
    ShakeCamera shake_camera;
    public Vector2 shake_vec;


    // Start is called before the first frame update
    void Start()
    {
        shake_camera=Camera.main.gameObject.GetComponent<ShakeCamera>();
        hp_particle = Resources.Load<GameObject>("HPParticle");
        rb = GetComponent<Rigidbody2D>();
        parameter = GetComponent<PartsParameter>();
        particle_system=GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        hp=parameter.hp;
        attack = parameter.attack;
        parameter.defence = (parameter.defence <= -100) ? -99 : parameter.defence;
        defence = (100 + parameter.defence) / 100;
        if (rb != null && rb.velocity.magnitude > 10) vel = rb.velocity.magnitude / 10;
        else vel = vel_sub;
        //Debug.Log(attack);
        vel = vel < 1 ? 0 : vel;
        if(damage!=0&&enable_damage)
        {
            parameter.hp-=damage;
            CreateParticle();
            damage=0;
            StartCoroutine(DamageCoolTime(attack_cool_time));
            //Debug.Log("damage"+transform);
        }
        damage=0;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("hoaijoia" + col.transform.name);
        if (col.transform.tag != transform.tag&&enable_attack)
        {
            battle_damage col_bd = col.transform.gameObject.GetComponent<battle_damage>();
            if (col_bd != null)
            {
                int send_damage = (int)((vel * attack / col_bd.defence )- col_bd.damage_cut);

                if (col_bd.hp > 0 && attack > 0)
                {   
                    col_bd.damage += (int)send_damage;
                    col_bd.shake_vec=(col.transform.position-transform.position).normalized;
                    StartCoroutine(AttackCoolTime(attack_cool_time));
                }
            }
        }
    }
    void OnParticleCollision(GameObject obj)
    {
        
        if (particle_system!=null&&obj.transform.tag != transform.tag&&enable_attack)
        {
            
            int numCollisionEvents = particle_system.GetCollisionEvents(obj, collisionEvents);
            battle_damage obj_bd = obj.transform.gameObject.GetComponent<battle_damage>();
            if (obj_bd != null)
            {
                int send_damage = (int)((vel * attack / obj_bd.defence )- obj_bd.damage_cut);

                if (obj_bd.hp > 0 && attack > 0)
                {    
                    //Debug.Log("xoxo");
                    obj_bd.damage += (int)send_damage;
                    obj_bd.shake_vec=(obj.transform.position-transform.position).normalized;
                    StartCoroutine(AttackCoolTime(attack_cool_time));
                }
            }
        }
    }
    void CreateParticle()
    {
        Transform parent_t=transform;
        for (int i = 0;parent_t.parent!=null&&i<100; i++)
        {
            if(parent_t.parent.tag!=transform.tag)
            {
                break;
            }
            else
            {
                parent_t=parent_t.parent;
            }
        }
        GameObject made_hp = Instantiate(hp_particle,parent_t.position, Quaternion.identity);
        TextMesh hp_text = made_hp.GetComponentInChildren<TextMesh>();
        hp_text.text = (-damage).ToString();
        if (transform.tag == "Player"){
            StartCoroutine(shake_camera.Shake(shake_vec));
            hp_text.color = Color.red;
        }
        else{
            StartCoroutine(shake_camera.HitStop(0.05f,transform.position));
            hp_text.color = Color.green;
        }
    }
    IEnumerator DamageCoolTime(float _time=0)
    {
        enable_damage=false;
        yield return new WaitForSeconds(_time);
        enable_damage=true;
    }
    IEnumerator AttackCoolTime(float _time=0)
    {
        enable_attack=false;
        Debug.Log("at_x");
        yield return new WaitForSeconds(_time);
        enable_attack=true;
        Debug.Log("at_o");
    }
}
/*if(send_damage<receive_damage){
    //Debug.Log(gameObject.name+"勝ち");
    parameter.hp-=(int)receive_damage;
}
else if(send_damage>receive_damage){
    //Debug.Log(gameObject.name+"負け");
    parameter.hp-=(int)(vel/defence/4);
}
else if(col_bd.vel!=0||vel!=0){
    //Debug.Log(gameObject.name+"あいこ");
    //Debug.Log(attack);
    //Debug.Log(defence);
    parameter.hp-=(int)(receive_damage/2);
}*/
/*if(send_damage<receive_damage){
    //Debug.Log(gameObject.name+"勝ち");
    root_parameter.hp-=(int)receive_damage;
}
else if(send_damage>receive_damage){
    //Debug.Log(gameObject.name+"負け");
    root_parameter.hp-=(int)(vel/defence/4);
}
else if(col_bd.vel!=0||vel!=0){
    //Debug.Log(gameObject.name+"あいこ");
    //Debug.Log(attack);
    //Debug.Log(defence);
    root_parameter.hp-=(int)(receive_damage/2);
}*/
