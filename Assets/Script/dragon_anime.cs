using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DragonBones;
public class dragon_anime : MonoBehaviour
{
    
    UnityArmatureComponent armature;
    // Start is called before the first frame update
    void Start()
    {
        
        armature=GetComponent<UnityArmatureComponent>();
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKeyDown(KeyCode.Q)){
                armature.animation.Play("walk");
            }if(Input.GetKeyDown(KeyCode.E)){
                armature.animation.Stop();
            }
        //armature.animation.Play();
    }
}
