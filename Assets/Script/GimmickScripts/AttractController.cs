using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractController : MonoBehaviour
{
    search_ray sr;
    public bool is_push;
    [SerializeField] float ray_angle;
    [SerializeField] float interval_angle;
    [SerializeField] float range_angle;
    [SerializeField] float length;
    [SerializeField] float power;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<search_ray>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] targets= sr.Search(transform.position, ray_angle, 
            interval_angle, range_angle, length,~(1<<3));
        foreach (RaycastHit2D target in targets)
        {
            if(target!=new RaycastHit2D())
            {
                Vector2 vec = target.point - (Vector2)transform.position;
                Rigidbody2D rb = target.transform.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null&&is_push==true)
                {
                    rb.AddForce(vec.normalized * power*(1-vec.magnitude/length));
                    //Debug.Log((vec.normalized * power * (1-vec.magnitude / length)).magnitude);
                }
            }
        }

    }
}
