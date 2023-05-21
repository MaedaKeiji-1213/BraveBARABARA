using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_obj : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] int parent_value;
    GameObject opened_obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void break_obj(){
        Destroy(obj);
    }
    public void open(){
        if(parent_value<=0){
            Instantiate(obj);
        }
        else{
            Transform par=transform;
            for(int i=parent_value;i>1;i--){
                par=par.parent;
            }
            Instantiate(obj,par);
        }
    }
    public void close(){
        Destroy(opened_obj);
    }
}
