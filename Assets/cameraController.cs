using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed;
    // Start is called before the first frame update
    GameObject player;
    const int num=2;
    Vector2 []player_position_old=new Vector2[num];
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player==null)
            player= GameObject.Find("head");
        Vector2 playerPos=player.transform.position;
        this.transform.Translate(playerPos-(Vector2)transform.position);
    }
}
