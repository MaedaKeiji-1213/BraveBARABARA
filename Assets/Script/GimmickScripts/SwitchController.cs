using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public bool is_push;
    public GameObject enemy;
    [SerializeField] float enemy_x;
    [SerializeField] float enemy_y;


    // Start is called before the first frame update
    void Start()
    {
        enemy.transform.position = transform.position+new Vector3(enemy_x, enemy_y, 0);
        is_push = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player"&& is_push == false)
        {
            is_push = true;
            transform.position += new Vector3(0,-1,0);
            Instantiate(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
