using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacklashController : MonoBehaviour
{
    GameObject player;
    [SerializeField] float backlash;
    Vector2 leftBottom;
    Vector2 rightTop;
    FadeManager fade_manager;

    // Start is called before the first frame update
    void Start()
    {
        fade_manager = FadeManager.Instance;
        player = GameObject.Find("head");
    }

    // Update is called once per frame
    void Update()
    {
        leftBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        rightTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 dir = Camera.main.transform.position - player.transform.position;

        if (player.transform.position.x <= leftBottom.x ||
            player.transform.position.y <= leftBottom.y ||
            player.transform.position.x >= rightTop.x ||
            player.transform.position.y >= rightTop.y)
        {
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*backlash);
        }
        if(player.transform.position.x <= leftBottom.x - 2)
        {
            fade_manager.LoadScene("ContinueScene", 1.0f);
        }
    }
}
