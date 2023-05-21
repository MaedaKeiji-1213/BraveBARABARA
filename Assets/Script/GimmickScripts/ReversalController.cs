using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversalController : MonoBehaviour
{

    public bool is_push;

    [SerializeField] float speed;

    [SerializeField] float change_rotation;

    float rota = 1;

    // Start is called before the first frame update

    void Start()

    {

        is_push = false;

        change_rotation %= 360;

        if (change_rotation < 0)

        {

            change_rotation += 360;

        }

    }

    private void OnCollisionEnter2D(Collision2D other)

    {

        if ((other.gameObject.tag == "Player") && is_push == false)

        {

            rota = 114514;

            is_push = true;
            transform.position += new Vector3(0, -1, 0);

        }

    }





    // Update is called once per frame

    void Update()

    {

        if (Mathf.Abs(rota) >= 0.001f && is_push == true)

        {

            Debug.Log("ro");

            rota = Mathf.Abs(change_rotation - Camera.main.transform.eulerAngles.z) * speed;

            Camera.main.transform.Rotate(0, 0, rota);

        }

    }

}
