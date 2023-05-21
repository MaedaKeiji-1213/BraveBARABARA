using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class foot_anime : MonoBehaviour
{
    [SerializeField]GameObject foot_obj;
    foot foot_cs;
    UnityArmatureComponent armature;
    // Start is called before the first frame update
    void Start()
    {
        foot_cs=foot_obj.GetComponent<foot>();
        armature=GetComponent<UnityArmatureComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(foot_cs.is_move==true){
            if(armature.animation.lastAnimationName!="walk")armature.animation.Play("walk");
        }
        else{
            if(armature.animation.lastAnimationName!="stay")armature.animation.Play("stay"); 
        }
    }
}
