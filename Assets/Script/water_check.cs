using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_check : MonoBehaviour
{
    bool is_touch_enter=false;//is_touch_enter
    bool is_touch_stop=false;//is_touch_stopay
    bool is_touch_exit=false;//is_touch_exit
    public bool is_touch=false;

    // Start is called before the first frame update
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update(){

    }
    public bool touch_c()
    {
        if(is_touch_enter==true||is_touch_stop==true){
            is_touch=true;
        }
        else if(is_touch_exit==true){
            is_touch=false;
        }
            
        is_touch_enter=false;
        is_touch_stop=false;
        is_touch_exit=false;

        return is_touch;
    }
    void OnTriggerEnter2D (Collider2D col){
        if(col.gameObject.layer==4){
            is_touch_enter=true;
            is_touch_stop=false;
            is_touch_exit=false;
        }
    }
    void OnTriggerStay2D (Collider2D col){
        if(col.gameObject.layer==4){
            is_touch_enter=false;
            is_touch_stop=true;
            is_touch_exit=false;
        }
    }
    void OnTriggerExit2D (Collider2D col){
        if(col.gameObject.layer==4){
            is_touch_enter=false;
            is_touch_stop=false;
            is_touch_exit=true;
        }
    }
}
