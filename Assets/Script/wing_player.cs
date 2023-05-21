using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class wing_player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float wing_force;
    
    UnityArmatureComponent armature;
    float x;
    float y;
    // Start is called before the first frame update
    void Start()
    {
        armature=transform.Find("DragonWing").GetComponent<UnityArmatureComponent>();
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        x=Mathf.Cos(((transform.rotation.eulerAngles.z+90)*Mathf.Deg2Rad));
        y=Mathf.Sin(((transform.rotation.eulerAngles.z+90)*Mathf.Deg2Rad));
        if(Input.GetKeyDown(KeyCode.W)&&!armature.animation.isPlaying){
            armature.animation.Play("fly",1);
            rb.AddForce(new Vector2(x,y)*wing_force);
            Debug.Log(new Vector2(x,y));
        }
    }
}
