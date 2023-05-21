using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    bool islock=false;
    GameObject parts;
    [SerializeField]int lock_num;
    [SerializeField] GameObject parts_lock;

    // Start is called before the first frame update
    void Start()
    {
        islock=GameData.now_data.parts_unlock[lock_num];
        parts = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (islock)
        {
            parts.GetComponent<BoxCollider2D>().enabled = false;
            parts_lock.SetActive(true);
        }
        else
        {
            parts.GetComponent<BoxCollider2D>().enabled = true;
            parts_lock.SetActive(false);
        }
    }
}
