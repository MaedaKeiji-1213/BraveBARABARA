using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttract : MonoBehaviour
{
    [SerializeField] float ray_angle;
    [SerializeField] float interval_angle;
    [SerializeField] float range_angle;
    [SerializeField] float length;
    [SerializeField] float power;

    

    Collider2D[] colliders=null;
    Collider2D my_col;

    Vector2 vec;
    Rigidbody2D rb;
    [SerializeField] GameObject hand;

    public void grasp()
    {
        Vector2 vec = Vector2.zero;
        my_col = hand.GetComponent<CircleCollider2D>();
        float ofs_angle = ((Mathf.Atan2(my_col.offset.y, my_col.offset.x) * Mathf.Rad2Deg) + hand.transform.rotation.eulerAngles.z);
        vec.x = Mathf.Cos(ofs_angle * Mathf.Deg2Rad) * (my_col.offset.magnitude * hand.transform.lossyScale.x);
        vec.y = Mathf.Sin(ofs_angle * Mathf.Deg2Rad) * (my_col.offset.magnitude * hand.transform.lossyScale.y);
        colliders = Physics2D.OverlapCircleAll((Vector2)hand.transform.position + vec, hand.GetComponent<CircleCollider2D>().radius * hand.transform.lossyScale.x);
    }
    public void separate()
    {
        if (colliders != null)
        {
            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.tag == "Player" && col != my_col && col.isTrigger == false
                    && col.transform != hand.transform.parent && col.gameObject.layer != 3)
                {
                    //Debug.Log("Player");
                    vec = (Vector2)col.transform.position - (Vector2)hand.transform.position;
                    rb = col.transform.gameObject.GetComponent<Rigidbody2D>();
                    rb.velocity = Vector3.down*power;
                    Debug.DrawRay(col.transform.position, -vec, Color.magenta);
                }
            }
        }
        colliders = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0.5f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (colliders != null)
        {
            
            foreach (Collider2D col in colliders)
            {
                
                if (col.gameObject.tag == "Player" && col != my_col && col.isTrigger == false 
                    && col.transform != hand.transform.parent && col.gameObject.layer != 3)
                {

                    //Debug.Log("Player");
                    vec = (Vector2)col.transform.position - (Vector2)hand.transform.position;
                    rb = col.transform.gameObject.GetComponent<Rigidbody2D>();
                    rb.velocity = vec.normalized * power;    
                    Debug.DrawRay(col.transform.position,-vec,Color.magenta);
                }
            }
        }
    }
}
