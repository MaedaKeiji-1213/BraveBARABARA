using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitBox : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject hitbox_obj;
    Toggle hitbox_toggle;
    void Start()
    {
        hitbox_toggle=GameObject.Find("HitBox_Toggle").GetComponent<Toggle>();
        hitbox_obj=transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitbox_toggle.isOn)
        {
            hitbox_obj.SetActive(true);
        }
        else
        {
            hitbox_obj.SetActive(false);
        }
    }
}
