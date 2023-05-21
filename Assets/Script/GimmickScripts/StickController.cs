using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<FixedJoint2D>().connectedBody=other.
                gameObject.GetComponent<Rigidbody2D>();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
