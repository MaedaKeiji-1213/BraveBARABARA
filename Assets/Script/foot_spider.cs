using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class foot_spider: MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]float speed;
    [SerializeField]float jump_force;
    [SerializeField]GameObject foot_R;
    [SerializeField]GameObject foot_L;
    [SerializeField]water_check water_R;
    [SerializeField]water_check water_L;

    touch_check_Collision buttom_R_check; 
    touch_check_Collision buttom_L_check;
    Rigidbody2D rb2D_R;
    Rigidbody2D rb2D_L;
    UnityArmatureComponent armature;
    [System.NonSerialized] public bool is_move;
    bool is_water=false;

    void Start()
    {
        rb2D_R=foot_R.GetComponent<Rigidbody2D>();
        rb2D_L=foot_L.GetComponent<Rigidbody2D>();
        buttom_R_check=foot_R.GetComponent<touch_check_Collision>();
        buttom_L_check=foot_L.GetComponent<touch_check_Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(water_R.touch_c()||water_L.touch_c())
        {
            rb2D_R.AddForce(jump_force*(transform.position-foot_R.transform.position).normalized);//Vector2.up*
            rb2D_L.AddForce(jump_force*(transform.position-foot_L.transform.position).normalized);//Vector2.up*
        }
        else{
            //Debug.Log("buttom_R_check.GetNormalVector()"+buttom_R_check.GetNormalVector());
            rb2D_R.AddForce(-jump_force*buttom_R_check.GetNormalVector());//Vector2.up*
            //Debug.Log("buttom_L_check.GetNormalVector()"+buttom_L_check.GetNormalVector());
            rb2D_L.AddForce(-jump_force*buttom_L_check.GetNormalVector());//Vector2.up*
        }
    }

    void FixedUpdate()
    {
    }
}
