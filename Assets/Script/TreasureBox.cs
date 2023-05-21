using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    [SerializeField] GameObject parts_obj;
    [SerializeField] GameObject particle_obj;
    [SerializeField] GameObject key_obj;
    [SerializeField] GameObject blackVeil;
    [SerializeField] int index;
    Animator anime;
    bool run_getParts = false;
    // Start is called before the first frame update
    void Start()
    {
        anime=GetComponent<Animator>();
        anime.speed=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(run_getParts&&anime.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.5f){
            StartCoroutine(GetParts(index));
        }
    }
    IEnumerator GetParts(int index)
    {
        //GameObject blackVeil=GameObject.Find("BlackVeil");
        float seconds_1 = 3f;
        float seconds_2 = 0.5f;
        parts_obj.SetActive(true);
        particle_obj.SetActive(true);
        GameData.now_data.parts_unlock[index] = true;
        blackVeil.GetComponent<SpriteRenderer>().enabled = true;
        SetTransitionScale.Set(parts_obj, parts_obj.transform.localScale * 16, seconds_1);
        yield return new WaitForSeconds(seconds_1 + 0.1f);
        SetTransitionScale.Set(parts_obj, parts_obj.transform.localScale * 0.9f, seconds_2);
        yield return new WaitForSeconds(seconds_2 + 1f);
        blackVeil.GetComponent<SpriteRenderer>().enabled = false;
        parts_obj.SetActive(false);
        particle_obj.SetActive(false);
        Destroy(GetComponent<TreasureBox>());
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!run_getParts && Input.GetKeyDown(KeyCode.Q))
        {
            anime.speed=1;
            run_getParts=true;
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        key_obj.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        key_obj.SetActive(false);
    }
}
