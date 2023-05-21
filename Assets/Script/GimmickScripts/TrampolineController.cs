using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    [SerializeField] float reflection;
    [SerializeField,Tooltip("Vec")] Vector2 vec ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb=other.gameObject.GetComponent<Rigidbody2D>();
        if(rb!=null){
            Vector2 touch_p=other.collider.ClosestPoint(other.transform.position);
            Vector2 touch_dir=touch_p - (Vector2)transform.position;
            touch_dir /= touch_dir.magnitude;


            rb.AddForce((touch_dir* reflection)+vec);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
