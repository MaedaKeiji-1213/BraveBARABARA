using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class work_Arm : MonoBehaviour
{
    HingeJoint2D hj2D;
    HingeJoint2D hj2D_DontUse;
    JointMotor2D jm2D;
    [SerializeField]float arm_power;
    float old_hj_angle;
    bool old_wark_motor;
    float timer=0;
    float move_value=0;
    bool is_stop=false;
    
    // Start is called before the first frame update
    void Start()
    {
        hj2D=GetComponent<HingeJoint2D>();
        hj2D_DontUse=gameObject.AddComponent<HingeJoint2D>();
        hj2D_DontUse.connectedBody=hj2D.connectedBody;
        hj2D_DontUse.anchor=hj2D.anchor;
        hj2D_DontUse.connectedAnchor=hj2D.connectedAnchor;
        jm2D.maxMotorTorque=10000;
        old_hj_angle=hj2D.jointAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(hj2D){
            timer+=Time.deltaTime;
            move_value+=Mathf.Abs(old_hj_angle-hj2D.jointAngle);
            if(timer>=0.1||!old_wark_motor){
                if(move_value>=1){
                    is_stop=false;
                    timer=0;
                }
                else{
                    is_stop=true;
                    timer=0;
                }
                move_value=0;
            }

            jm2D.motorSpeed=0;
            if(Input.GetKey(KeyCode.UpArrow)){//Debug.Log("uparrow");
                hj2D.enabled=true;
                hj2D_DontUse.enabled=false;
                old_wark_motor=true;
                float over=(hj2D.limits.max-hj2D.jointAngle)/64;
                over=over>1?1:over;
                over=over<0?over/10:over;
                over=is_stop?-5f*over:over;
                //Debug.Log(over);
                jm2D.motorSpeed=arm_power;//*Mathf.Sqrt(Mathf.Abs(over))*Mathf.Sign(over);
            }
            else if(Input.GetKey(KeyCode.DownArrow)){
                hj2D.enabled=true;
                hj2D_DontUse.enabled=false;
                old_wark_motor=true;
                float over=(hj2D.jointAngle-hj2D.limits.min)/64;
                over=over>1?1:over;
                over=over<0?over/10:over;
                over=is_stop?-5f*over:over;
                //Debug.Log(over);
                jm2D.motorSpeed=-arm_power;//*Mathf.Sqrt(Mathf.Abs(over))*Mathf.Sign(over);
            }
            else{
                old_wark_motor=true;
                hj2D.enabled=false;
                hj2D_DontUse.enabled=true;
            }
            hj2D.motor=jm2D;
            old_hj_angle=hj2D.jointAngle;
        }
        else{
            Destroy(GetComponent<work_Arm>());
        }
    }
}
