using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsParameter : MonoBehaviour
{
    public int cost;
    public int max_hp;
    public int hp;
    public int attack;
    public int defence;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hp=hp<0?0:hp;
        hp=max_hp<hp?max_hp:hp;
    }
}
