using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repair_part : MonoBehaviour
{
    bool do_used=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(do_used=false&&Input.GetKeyDown(KeyCode.S))
            do_used=true;
        
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(do_used=true&&col.gameObject.tag=="Player"){
            PartsParameter pp=col.gameObject.GetComponent<PartsParameter>();
            pp.hp=pp.max_hp;
            Destroy(gameObject);
        }
            
    }

}
