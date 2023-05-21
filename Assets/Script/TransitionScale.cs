using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class TransitionScale : MonoBehaviour
{
    bool is_start =false ;
    bool is_running=false;


    Vector3 target_scale = Vector3.one;
    float seconds = -9999;

    void Start()
    {
    }
    void Update()
    {
        if (is_start)
        {
            is_running=true;
        }

        if(is_start)
        {
            Debug.Log(seconds);
            Vector3 difference = target_scale - transform.localScale;
            transform.localScale += difference*(Time.deltaTime/seconds);
            seconds-=Time.deltaTime;
            if(seconds<=0){
                transform.localScale=target_scale;
                Destroy(gameObject.GetComponent<TransitionScale>());
            }
        }

    }

    public void Initialize(Vector3 _target_scale, float _seconds = 1f)
    {
        if(!is_running){
            Debug.Log("scale" + _target_scale);
            target_scale = _target_scale;
            seconds = _seconds;
            is_start=true;
        }
    }
    public bool IsRunning()
    {
        return is_running;
    }
    public bool isStart()
    {
        return is_start;
    }
}

public static class SetTransitionScale
{

    public static void Set(GameObject _target, Vector3 _target_scale = default(Vector3), float _seconds = 1f)
    {
        TransitionScale ts = _target.AddComponent<TransitionScale>();
        //if(!ts.IsRunning())
        _target.GetComponent<TransitionScale>().Initialize(_target_scale, _seconds);
        //Debug.Log("Set!!");
    }
}
