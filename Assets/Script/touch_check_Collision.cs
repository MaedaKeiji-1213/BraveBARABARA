using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch_check_Collision : MonoBehaviour
{
    bool is_touch_enter=false;//is_touch_enter
    bool is_touch_stay=false;//is_touch_stay
    bool is_touch_exit=false;//is_touch_exit
    public bool is_touch=false;
    public float keep_time=0.1f;
    bool enable_reload=true;
    List<Vector2> normal_vectors=new List<Vector2>();
    Vector2 normal_vector=Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update(){
        if(enable_reload){
            normal_vector=CalcNormalVector(normal_vectors);
            StartCoroutine(Wait(keep_time));
            normal_vectors=new List<Vector2>();
        }
    }
    void OnCollisionEnter2D (Collision2D col){
        if(!col.collider.isTrigger){
            foreach (ContactPoint2D contact in col.contacts)
            {
                normal_vectors.Add(-(contact.point-(Vector2)transform.position).normalized);
                Debug.DrawRay(transform.position,-(contact.point-(Vector2)transform.position).normalized,Color.red,0.1f);
            }
            is_touch_enter=true;
            is_touch_stay=false;
            is_touch_exit=false;
        }
    }
    void OnCollisionStay2D (Collision2D col){
        if(!col.collider.isTrigger){
            foreach (ContactPoint2D contact in col.contacts)
            {
                normal_vectors.Add(-(contact.point-(Vector2)transform.position).normalized);
                Debug.DrawRay(transform.position,-(contact.point-(Vector2)transform.position).normalized,Color.blue,0.1f);
            }
            is_touch_enter=false;
            is_touch_stay=true;
            is_touch_exit=false;
        }
    }
    void OnCollisionExit2D (Collision2D col){
        if(!col.collider.isTrigger){
            foreach (ContactPoint2D contact in col.contacts)
            {
                normal_vectors.Add(-(contact.point-(Vector2)transform.position).normalized);
                Debug.DrawRay(transform.position,-(contact.point-(Vector2)transform.position).normalized,Color.green,0.1f);
            }
            is_touch_enter=false;
            is_touch_stay=false;
            is_touch_exit=true;
        }
    }

    public bool touch_c()
    {
        if(is_touch_enter==true||is_touch_stay==true){
            is_touch=true;
        }
        else if(is_touch_exit==true){
            is_touch=false;
        }
            
        is_touch_enter=false;
        is_touch_stay=false;
        is_touch_exit=false;

        return is_touch;
    }
    public Vector2 GetNormalVector()
    {
        return normal_vector;
    }
    Vector2 CalcNormalVector(List<Vector2> vectors)
    {
        Vector2 result_vector=Vector2.zero;
        Vector2 vector_sum=Vector2.zero;
        if(vectors.Count>0){
            foreach (Vector2 vec in vectors)
            {
                vector_sum+=vec;
            }
            result_vector=vector_sum/vectors.Count;
        }
        // Debug.Log("vector_sum"+vector_sum);
        // Debug.Log("vectors.Count"+vectors.Count);
        return result_vector;
    }
    IEnumerator Wait(float _time)
    {
        enable_reload=false;
        yield return new WaitForSeconds(_time);
        enable_reload=true;
    }
}
