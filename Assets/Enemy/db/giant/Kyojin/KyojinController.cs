using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class KyojinController : MonoBehaviour
{
    GameObject player;
    public GameObject giant_rock;
    GameObject made_rock;
    public GameObject hand;
    [SerializeField] Vector2 distance;
    [SerializeField] float span;
    float n = 0;
    float rock_x = -0.3f;
    float rock_y = -2;
    public void rock()
    {
        made_rock=Instantiate(giant_rock,hand.transform.position,Quaternion.identity,hand.transform);
    }

    public void rock_throw()
    {
        Rigidbody2D make_rock=made_rock.GetComponent<Rigidbody2D>();
        make_rock.simulated = true;
        made_rock.transform.parent = null;
        make_rock.AddForce(new Vector2(rock_x, rock_y)*800);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("head");
        InitAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 player_position = player.transform.position;
            Vector2 player_distance =  player_position-(Vector2)transform.position ;
            if (player_distance.x < 0)
            {
                player_distance.x *= -1;
            }
            if (player_distance.y < 0)
            {
                player_distance.y *= -1;
            }
            Debug.Log(player_position.x);
            Debug.Log(transform.position.x);
            if (player_position.x > transform.root.position.x)
            {
                
                Vector3 scale = transform.root.localScale;
                scale.x = Mathf.Abs(scale.x)*1;
                transform.root.localScale = scale;
                if (rock_x < 0)
                {
                    rock_x *= -1;
                }
            }
            else if (player_position.x < transform.root.position.x)
            {
                Debug.Log("b");
                Vector3 scale = transform.root.localScale;
                scale.x = Mathf.Abs(scale.x) * -1;
                transform.root.localScale = scale;
                if (rock_x > 0)
                {
                    rock_x *= -1;
                }
            }
            n += Time.deltaTime;
            //Debug.Log(player_distance);

            

            //プレイヤーが近づいてきたら
            if (player_distance.x<=distance.x&& player_distance.y <= distance.y && n >= span)
            {
                
                int rnd = Random.Range(1, 4);
                if (rnd == 1)
                {
                    Debug.Log("殴る");
                    changeAnim(AnimNo.giant_attack);
                    n = -2;
                }
                else if (rnd == 2)
                {
                    Debug.Log("プレイヤーを掴んで投げる");
                    changeAnim(AnimNo.germanSuplex);
                    n = -15;
                }
                else if (rnd == 3)
                {
                    Debug.Log("倒れる");
                    changeAnim(AnimNo.giant_drop);
                    n -= 20;
                }
            }
            

            //プレイヤーが遠くにいる時
            if ((player_distance.x >= distance.x || player_distance.y >= distance.y)&&n>=span)
            {
                Debug.Log("岩を投げる");
                changeAnim(AnimNo.giant_throw);
                n -= 5;
            }
            

        }
        else
        {
            player = GameObject.Find("head");
        }    
    }
}
