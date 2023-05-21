using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead_bat : MonoBehaviour
{
    [SerializeField] float alive_time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alive_time-=Time.deltaTime;
        if(alive_time<0)Destroy(gameObject);
    }
    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag!="Player")Destroy(gameObject,1);
    }
}
