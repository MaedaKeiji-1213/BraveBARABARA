using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class use_SavePoint : MonoBehaviour
{
    [SerializeField] GameObject key_obj;
    [SerializeField] GameObject key_obj2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {   
            Debug.Log("Q");
            char[] trim="SavePoint".ToCharArray();
            SavePoint.respone_point= short.Parse(transform.parent.name.TrimStart(trim).ToCharArray());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            char[] trim="SavePoint".ToCharArray();
            SavePoint.respone_point= short.Parse(transform.parent.name.TrimStart(trim).ToCharArray());
            Debug.Log("Trimしたよ"+short.Parse(transform.parent.name.TrimStart(trim).ToCharArray()));
            Debug.Log("Trimしたよ２"+SavePoint.respone_point);
            FadeManager.Instance.LoadScene("SetScene", 1.0f);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        key_obj.SetActive(true);
        key_obj2.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        key_obj.SetActive(false);
        key_obj2.SetActive(false);
    }
}
