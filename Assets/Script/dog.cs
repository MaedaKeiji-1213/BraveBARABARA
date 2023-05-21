using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog : MonoBehaviour
{
    GameObject player;

    Rigidbody2D[] foot=new Rigidbody2D[8];
    [SerializeField] float walk_force;
    [SerializeField] float homing_range;
    Vector2 dog_scale;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player");
        foot[0]=transform.Find("Circle1").gameObject.GetComponent<Rigidbody2D>();
        foot[1]=transform.Find("Circle2").gameObject.GetComponent<Rigidbody2D>();
        foot[2]=transform.Find("Circle3").gameObject.GetComponent<Rigidbody2D>();
        foot[3]=transform.Find("Circle4").gameObject.GetComponent<Rigidbody2D>();
        foot[4]=transform.Find("Circle5").gameObject.GetComponent<Rigidbody2D>();
        foot[5]=transform.Find("Circle6").gameObject.GetComponent<Rigidbody2D>();
        foot[6]=transform.Find("Circle7").gameObject.GetComponent<Rigidbody2D>();
        foot[7]=transform.Find("Circle8").gameObject.GetComponent<Rigidbody2D>();
        dog_scale=transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position-transform.position).magnitude<homing_range){
            short sign=(short)Mathf.Sign(player.transform.position.x-transform.position.x);
            foot[4].angularVelocity=foot[5].angularVelocity=
            foot[6].angularVelocity=foot[7].angularVelocity=-walk_force*(sign==0?1:sign);
        }
        
    }
}
