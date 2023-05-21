using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    // Start is called before the first frame update
    PartsParameter parameter;
    battle_damage bd;
    [SerializeField] GameObject dead_effect;
    [SerializeField] Color effect_color;
    [SerializeField,Tooltip("effect_size")] Vector3 effect_size;
    void Start()
    {
        bd=GetComponent<battle_damage>();
        parameter=GetComponent<PartsParameter>();
        if(effect_size==Vector3.zero)
            effect_size=Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        if(tag=="Player"&&parameter.hp<=0){
            Debug.Log("dead_player"+gameObject);
            GenerateEffect();
            Vector2 rand=bd.shake_vec;
            Rigidbody2D dead_parts=Instantiate<GameObject>(Resources.Load<GameObject>("Parts/"+gameObject.name),transform.position,Quaternion.identity).GetComponent<Rigidbody2D>();
            dead_parts.sharedMaterial=Resources.Load<PhysicsMaterial2D>("Bound");
            dead_parts.AddForce(rand*4800);
            Destroy(dead_parts.gameObject,10);
            Destroy(this.gameObject);
        }
    }
    public void ToDead(){
        GenerateEffect();
        Destroy(transform.parent.gameObject,0.2f);
    }
    void GenerateEffect()
    {
        if(dead_effect!=null){
            GameObject effect=Instantiate(dead_effect,transform.position,Quaternion.identity);
            Destroy(effect,3);
            effect.GetComponent<ParticleSystem>().startColor=effect_color;
            effect.transform.localScale=effect_size;
        }
    }
}
