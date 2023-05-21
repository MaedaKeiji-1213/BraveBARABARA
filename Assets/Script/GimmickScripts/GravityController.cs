using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool is_push;
    [SerializeField] float gravity_x;
    [SerializeField] float gravity_y;

    // Start is called before the first frame update
    void Start()
    {
        is_push = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player"&& is_push == false)
        {
            is_push = true;
            Physics2D.gravity = new Vector3(gravity_x, gravity_y, 0);
            transform.position += new Vector3(0, -1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
