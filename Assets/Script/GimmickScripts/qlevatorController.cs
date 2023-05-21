using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class qlevatorController : MonoBehaviour
{
    [SerializeField] float speed_x;
    [SerializeField] float speed_y;
    Vector2 speed_dir;
    [SerializeField] float moving_distance;
    Vector3 origin_position;
    Vector3 end_position;
    [SerializeField] float stand_time;
    Rigidbody2D rb;
    public bool is_turn=false;
    float left_time = 0;
    bool is_stop=true;
    
    [SerializeField] float force_x;
    [SerializeField] float force_y;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed=speed<=0?0.01f:speed;
        rb=GetComponent<Rigidbody2D>();
        speed_dir = new Vector2(speed_x, speed_y);
        origin_position = transform.position;
        speed_dir /= speed_dir.magnitude;

        end_position = (Vector2)transform.position + speed_dir * moving_distance;
        transform.Translate(new Vector3(speed_x/Math.Abs(speed_x)*speed, speed_y/Math.Abs(speed_y)*speed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (left_time <= 0)
        {
            rb.velocity=(new Vector3(speed_x, speed_y, 0));
            if (((origin_position.x >= transform.position.x &&
                origin_position.y >= transform.position.y) ||
                (end_position.x <= transform.position.x &&
                end_position.y <= transform.position.y)))
            {
                speed_x *= -1;
                speed_y *= -1;
                
                left_time = stand_time;
                is_turn=true;
            }
            else
            {
                is_turn=false;
                is_stop=false;
            }
        }
        else if(left_time>stand_time-0.03f)
        {
            transform.Translate(new Vector3(speed_x/Math.Abs(speed_x)*speed, speed_y/Math.Abs(speed_y)*speed, 0));
            //rb.velocity=(new Vector3(-speed_x, -speed_y, 0));
            left_time -= Time.deltaTime;
            is_stop=true;
        }
        else
        {
            rb.velocity=Vector2.zero;
            left_time -= Time.deltaTime;
            is_turn=false;
            is_stop=true;
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {   
        Rigidbody2D rb=col.gameObject.GetComponent<Rigidbody2D>();
        if(rb!=null&&is_turn){
            rb.AddForce(new Vector3(speed_x*force_x*rb.mass, speed_y*force_y*rb.mass, 0));
        }
    }
}
