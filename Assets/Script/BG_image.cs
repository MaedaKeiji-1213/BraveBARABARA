using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BG_image : MonoBehaviour
{   [SerializeField] bool do_size_fit=false;
    [SerializeField] float speed_scroll_x=0;
    [SerializeField] float speed_scroll_y=0;
    [SerializeField] GameObject bg_image;
    SpriteRenderer bg_image_sr;
    Vector2 position_camera_old,position_camera_gap;
    Vector2 cameraRectMin,cameraRectMax;
    Vector2 size_camera;
    Vector2 size_image;
    GameObject image_obj;
    GameObject bg_image_parent;
    Vector3 position_bgi;

    void Start()
    {
        position_camera_old=Camera.main.transform.position;
        if(do_size_fit==true){
            size_camera=Camera.main.ViewportToWorldPoint(Vector2.zero)-Camera.main.ViewportToWorldPoint(Vector2.one);
            bg_image.transform.localScale=new Vector3(size_camera.x,-size_camera.y,1);
            //Debug.Log(size_camera);
        }
        size_image=bg_image.transform.lossyScale*bg_image.GetComponent<SpriteRenderer>().size;

        bg_image_parent=new GameObject("BGI");
        bg_image_parent.transform.position=bg_image.transform.position;
        bg_image_parent.transform.localScale=size_image;
        position_bgi=bg_image_parent.transform.position;
        Debug.Log(size_image);
        
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi,Quaternion.identity);//真ん中
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(0,size_image.y,0),Quaternion.identity);//上
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(size_image.x,size_image.y,0),Quaternion.identity);//右上
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(size_image.x,0,0),Quaternion.identity);//右
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(size_image.x,-size_image.y,0),Quaternion.identity);//右下
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(0,-size_image.y,0),Quaternion.identity);//下
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(-size_image.x,-size_image.y,0),Quaternion.identity);//左下
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(-size_image.x,0,0),Quaternion.identity);//左
        image_obj.transform.parent = bg_image_parent.transform;
        image_obj=(GameObject)Instantiate(bg_image.transform.root.gameObject,position_bgi+new Vector3(-size_image.x,size_image.y,0),Quaternion.identity);//左上
        image_obj.transform.parent = bg_image_parent.transform;


        bg_image_sr=bg_image.GetComponent<SpriteRenderer>();
        bg_image_sr.sortingOrder=-100;
        Destroy(bg_image);
        //bg_image_parent.transform.position=new Vector3 (Camera.main.transform.position.x,(Camera.main.ViewportToWorldPoint(Vector2.zero).y+transform.localScale.y/2),1);
    }
    void Update()
    {
        //カメラの範囲を取得
        cameraRectMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        cameraRectMax = Camera.main.ViewportToWorldPoint(Vector2.one);

        /*カメラの移動距離をもとめる*/
        position_camera_gap= (Vector2)position_camera_old-(Vector2)Camera.main.transform.position;
        bg_image_parent.transform.position=new Vector3((bg_image_parent.transform.position.x+position_camera_gap.x*speed_scroll_x),(bg_image_parent.transform.position.y+position_camera_gap.y*speed_scroll_y),1);   //スクロール
        position_camera_old=Camera.main.transform.position;
        //Debug.Log("c_pos="+Camera.main.transform.position+"i_pos="+transform.position+"gap="+position_camera_gap.x);

        

        /*x軸の背景ループ*/
        if((bg_image_parent.transform.position.x-bg_image_parent.transform.localScale.x/2) <= cameraRectMin.x)
        {
            //Debug.Log("min c_pos="+Camera.main.transform.position+"i_pos="+transform.position);
            bg_image_parent.transform.position +=new Vector3(bg_image_parent.transform.localScale.x,0,0);
        }
        if((bg_image_parent.transform.position.x+bg_image_parent.transform.localScale.x/2) >= cameraRectMax.x)
        {
            //Debug.Log("max c_pos="+Camera.main.transform.position+"i_pos="+transform.position);
            bg_image_parent.transform.position +=new Vector3(-bg_image_parent.transform.localScale.x,0,0);
        }

        /*y軸の背景ループ*/
        if((bg_image_parent.transform.position.y-bg_image_parent.transform.localScale.y/2) <= cameraRectMin.y)
        {
            //Debug.Log("min c_pos="+Camera.main.transform.position+"i_pos="+transform.position);
            bg_image_parent.transform.position +=new Vector3(0,bg_image_parent.transform.localScale.y,0);
        }
        if((bg_image_parent.transform.position.y+bg_image_parent.transform.localScale.y/2) >= cameraRectMax.y)
        {
            //Debug.Log("max c_pos="+Camera.main.transform.position+"i_pos="+transform.position);
            bg_image_parent.transform.position +=new Vector3(0,-bg_image_parent.transform.localScale.y,0);
        }

    }
}