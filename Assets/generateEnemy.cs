using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateEnemy : MonoBehaviour
{
    Vector3 range_origin,range_end;
    [SerializeField] GameObject enemy_pre;
    GameObject enemy_obj;
    Vector2 area_range=new Vector2(200,200);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        range_origin=Camera.main.ViewportToWorldPoint(Vector2.zero);
        range_end=Camera.main.ViewportToWorldPoint(Vector2.one);
        range_origin=new Vector2(range_origin.x-area_range.x,range_origin.y-area_range.y);
        range_end=new Vector2(range_end.x+area_range.x,range_end.y+area_range.y);

        Vector2 EnemyPos=new Vector3(transform.position.x,transform.position.y,0);
        Vector2 enemyObjPos;
        if((range_origin.x<=EnemyPos.x&&EnemyPos.x<=range_end.x
                &&range_origin.y<=EnemyPos.y&&EnemyPos.y<=range_end.y)){
            if(enemy_obj==null){
                enemy_obj=Instantiate(enemy_pre,EnemyPos,Quaternion.identity);
                enemy_obj.transform.parent=transform;
            }
        }
        else if(enemy_obj!=null){
            enemyObjPos= new Vector3(enemy_obj.transform.position.x,enemy_obj.transform.position.y,0);
            if(!(range_origin.x<=enemyObjPos.x&&enemyObjPos.x<=range_end.x
                &&range_origin.y<=enemyObjPos.y&&enemyObjPos.y<=range_end.y)){
                Destroy(enemy_obj);
            }
        }
    }
}
