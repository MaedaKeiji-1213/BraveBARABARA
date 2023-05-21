using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clear : MonoBehaviour
{
    FadeManager fm;
    // Start is called before the first frame update
    void Start()
    {
        fm=FadeManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.name=="head")
            fm.LoadScene("TitleScene",1);
    }

}
