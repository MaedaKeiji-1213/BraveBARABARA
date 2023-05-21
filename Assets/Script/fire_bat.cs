using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_bat : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] int bat_value;
    [SerializeField] float cool_time;
    [SerializeField] GameObject bat_pre;
    int max_value;
    float passed_time=0;
    // Start is called before the first frame update

    void Start()
    {
        max_value=bat_value;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightShift)&&passed_time>=cool_time&&bat_value>0){
            bat_value--;
            Vector2 vec;
            vec.x=Mathf.Cos(((transform.rotation.eulerAngles.z-90)*Mathf.Deg2Rad));
            vec.y=Mathf.Sin(((transform.rotation.eulerAngles.z-90)*Mathf.Deg2Rad));
            Instantiate(bat_pre,(Vector2)transform.position+vec*2,transform.rotation).GetComponent<Rigidbody2D>().AddForce(vec*force);
            passed_time=0;
        }
        passed_time+=Time.deltaTime;
        passed_time=passed_time>10?10:passed_time;
    }
}
