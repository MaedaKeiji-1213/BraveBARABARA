using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedScrollController : MonoBehaviour
{
    public bool is_push;
    [SerializeField] float scroll_x;

    // Start is called before the first frame update
    void Start()
    {
        is_push = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player"&&is_push==false)
        {
            transform.position += new Vector3(0, -1, 0);
            is_push = true;
            Camera.main.transform.parent.GetComponent<cameraController>().enabled = false;
            GetComponent<BacklashController>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (is_push == true)
        {
            Camera.main.transform.position += new Vector3(scroll_x, 0, 0);
        }
    }
}
