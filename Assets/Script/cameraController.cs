using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] float speed;
    // Start is called before the first frame update
    GameObject player;
    const int num=2;
    void Start()
    {
        player=GameObject.Find("head");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player);
        if(player==null)player=GameObject.Find("head");
        else{
            Vector2 playerPos=player.transform.position;
            this.transform.Translate(playerPos-(Vector2)transform.position);
        }
    }
}
