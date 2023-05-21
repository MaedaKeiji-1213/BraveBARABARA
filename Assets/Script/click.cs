using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class click : MonoBehaviour
{
    [SerializeField] Transform pallet;
    [SerializeField] int max_cost;
    //?y?K?{?z?A?C?e????Collider??Item?^?O??t???�??
    GameObject new_obj;
    GameObject clicked_obj;
    GameObject active_obj;

    SpriteRenderer sr;
    Vector3 old_mouse=Vector3.zero;
    Vector3 generate_pos;

    public GameObject cost_object = null;
    Vector3 cost_object_scale;
    TextMeshProUGUI cost_text;
    int cost_num = 0;

    Material act_obj_mat;
    Material outline_mat;


    // Start is called before the first frame update
    void Start()
    {
        cost_text = cost_object.GetComponent<TextMeshProUGUI>();
        cost_object_scale=cost_object.transform.localScale;
        
        cost_text.text = "cost:0/"+max_cost;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&&active_obj!=null)
        {
            active_obj.transform.Rotate(new Vector3(0, 0, 15));
        }

        else if (Input.GetKeyDown(KeyCode.D)&&active_obj!=null)
        {
            active_obj.transform.Rotate(new Vector3(0, 0, -15));
        }
        if (Input.GetMouseButtonDown(0))
        {
            clicked_obj = null;
            //?}?E?X?|?C???^?[??u??????????W????[?U?[???o??????
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //???[?U?[?????????????F?????�Y??
            RaycastHit2D hitSprite = Physics2D.Raycast((Vector2)ray.origin,(Vector2)ray.direction,Mathf.Infinity,3);
            
            if (hitSprite == true)
            {
                //?N???b?N?????I?u?W?F?N?g???????????�Y??
                clicked_obj = hitSprite.transform.gameObject;
                Debug.Log(clicked_obj.name);
                

                //?A?C?e????????�Y??
                if (clicked_obj.transform.parent == pallet)
                {
                    generate_pos=clicked_obj.transform.position;
                    clicked_obj=Resources.Load<GameObject>("SetScene/Item/"+clicked_obj.name);
                    new_obj = Instantiate(clicked_obj);
                    new_obj.name = new_obj.name.Replace("(Clone)","");
                    new_obj.transform.position = generate_pos;
                    
                    MaterialToNormal();
                    active_obj = new_obj;
                    MaterialToOutline();
                }
                //??????????A?C?e??????x?????????????
                else if (clicked_obj.transform.parent== transform)
                { 
                    new_obj = hitSprite.transform.gameObject;
                    MaterialToNormal();
                    active_obj = new_obj;
                    MaterialToOutline();
                    clicked_obj.transform.parent=null;
                }
            }
            else
            {
                MaterialToNormal();
                active_obj=null;
            }
            
        }
        //?N???b?N????A?C?e????????????�Y??
        if (Input.GetMouseButton(0)&&active_obj!=null)
        {
            active_obj.transform.position=(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)&&new_obj!=null)
        {
            new_obj.transform.parent = this.gameObject.transform;
            //?I???????A?C?e?????q??????????
             new_obj = null;
            // if(active_obj!=null)
            // {
            //     Material mat=active_obj.GetComponent<Material>().shader;
            // }
        }
        cost_num = 0;
        GetChildren_cost(gameObject);
        if(max_cost< cost_num)
        {
            Destroy(active_obj);
            StartCoroutine(Emphasis(cost_object));
        }
        old_mouse=(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void GetChildren_cost(GameObject obj)
    {
        Transform parent = obj.transform;//GetComponentInChildren<Transform>();
        //?q?v?f??????????I??
        if (parent.childCount == 0)
        {
            return;
        }
        int n = 0;
        for(int i=0;i<parent.childCount;i++)
        {
            PartsParameter pp;
            pp=parent.GetChild(i).GetComponent<PartsParameter>();

            //Debug.Log(pp);
            /*Debug.Log(ob.name);
            Debug.Log(ob.transform.position);
            Debug.Log(ob.transform.rotation.z);
            Debug.Log(ob.transform.rotation.eulerAngles.z);
            Chara_SaveData.now_data.parts[n].position = ob.transform.position;
            Chara_SaveData.now_data.parts[n].object_name = ob.name;
            Chara_SaveData.now_data.parts[n].angle = ob.transform.rotation.eulerAngles.z;*/
            cost_num += pp.cost;
            cost_text.text = "cost:" + cost_num+"/"+max_cost;
            n++;
            //GetChildren_cost(parent.gameObject);
        }
    }
    IEnumerator Emphasis(GameObject target)
    {
        float seconds=0.25f;
        SetTransitionScale.Set(target,cost_object_scale*1.3f,seconds);
        TransitionScale zoom_in=target.GetComponent<TransitionScale>();
        cost_text.color=Color.red;
        yield return new WaitForSeconds(seconds+0.1f);
        SetTransitionScale.Set(target,cost_object_scale,seconds);
        yield return new WaitForSeconds(seconds);
        cost_text.color=Color.white;
    }
    void MaterialToOutline()
    {
        SpriteRenderer act_renderer=active_obj.GetComponent<SpriteRenderer>();
        if(act_renderer==null||act_renderer.color.a<=0)
            act_renderer=active_obj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        if(act_renderer!=null){
            act_obj_mat=act_renderer.material;
            act_renderer.material=Resources.Load<Material>("SetScene/OutLine_parts");
        }

    }
    void MaterialToNormal()
    {
        if(active_obj!=null){
            SpriteRenderer act_renderer=active_obj.GetComponent<SpriteRenderer>();
            if(act_renderer==null||act_renderer.color.a<=0)
                act_renderer=active_obj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            if(act_renderer!=null){
                act_renderer.material=act_obj_mat;
            }
        }

    }
}
