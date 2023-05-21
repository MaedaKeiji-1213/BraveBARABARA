using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]bool dontUse_y=false;
    [SerializeField]bool dontUse_x=false;
    [SerializeField]GameObject my_obj;
    [SerializeField,Tooltip("Speed")] Vector2 speed;
    Vector2 obj_size=Vector2.zero;
    Vector2 camera_old_pos;
    GameObject[] objs=new GameObject[8]; 
    // Start is called before the first frame update
    void Start()
    {
        camera_old_pos=GameObject.Find("SavePoint0").transform.position;
        obj_size= my_obj.GetComponent<SpriteRenderer>().bounds.size;
        if(!dontUse_x&&!dontUse_y)
        {
            objs[0]=Instantiate(my_obj,transform.position+new Vector3(0            ,obj_size.y ),Quaternion.identity);
            objs[1]=Instantiate(my_obj,transform.position+new Vector3(obj_size.x   ,obj_size.y ),Quaternion.identity);
            objs[2]=Instantiate(my_obj,transform.position+new Vector3(obj_size.x   ,0          ),Quaternion.identity);
            objs[3]=Instantiate(my_obj,transform.position+new Vector3(obj_size.x   ,-obj_size.y),Quaternion.identity);
            objs[4]=Instantiate(my_obj,transform.position+new Vector3(0            ,-obj_size.y),Quaternion.identity);
            objs[5]=Instantiate(my_obj,transform.position+new Vector3(-obj_size.x  ,-obj_size.y),Quaternion.identity);
            objs[6]=Instantiate(my_obj,transform.position+new Vector3(-obj_size.x  ,0          ),Quaternion.identity);
            objs[7]=Instantiate(my_obj,transform.position+new Vector3(-obj_size.x  ,obj_size.y ),Quaternion.identity);
        }
        else{
            if(!dontUse_x){            
                objs[2]=Instantiate(my_obj,transform.position+new Vector3(obj_size.x   ,0          ),Quaternion.identity);
                objs[6]=Instantiate(my_obj,transform.position+new Vector3(-obj_size.x  ,0          ),Quaternion.identity);
            }
            if(!dontUse_y){
                objs[0]=Instantiate(my_obj,transform.position+new Vector3(0            ,obj_size.y ),Quaternion.identity);
                objs[4]=Instantiate(my_obj,transform.position+new Vector3(0            ,-obj_size.y),Quaternion.identity);
            }
        }
        
        
        for (int i=0;i<objs.Length;i++)
        {
            if(objs[i]!=null){
                objs[i].transform.parent=my_obj.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dif=camera_old_pos-(Vector2)Camera.main.transform.position;
        my_obj.transform.position+=(new Vector3(speed.x*dif.x,speed.y*dif.y));
        
        if(Camera.main.transform.position.x>(my_obj.transform.position.x+obj_size.x/2)&&!dontUse_x)
            my_obj.transform.position+=new Vector3(obj_size.x,0);
        if(Camera.main.transform.position.x<(my_obj.transform.position.x-obj_size.x/2)&&!dontUse_x)
            my_obj.transform.position-=new Vector3(obj_size.x,0);
        if(Camera.main.transform.position.y>(my_obj.transform.position.y+obj_size.y/2)&&!dontUse_y)
            my_obj.transform.position+=new Vector3(0,obj_size.y);
        if(Camera.main.transform.position.y<(my_obj.transform.position.y-obj_size.y/2)&&!dontUse_y)
            my_obj.transform.position-=new Vector3(0,obj_size.y);
        camera_old_pos=(Vector2)Camera.main.transform.position;
    }
}
