using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking_pos : MonoBehaviour
{
    [SerializeField]Transform target;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D=target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=target.position;
        transform.rotation=target.rotation;
    }
}
