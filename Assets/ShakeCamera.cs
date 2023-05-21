using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // Start is called before the first frame update[System.NonSerialized]
    public bool start_shake=false;
    bool is_shaking=false;
    bool sign=false;
    Vector2 vec=Vector2.zero;
    [SerializeField] float _time;
    [SerializeField] float distance;
    GameObject hit_effect_1;
    GameObject hit_effect_2;

    void Start()
    {
        
        hit_effect_1=Resources.Load<GameObject>("impact1");
        hit_effect_2=Resources.Load<GameObject>("impact2");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition=Vector2.Lerp(transform.localPosition,vec,Time.deltaTime*2/_time);
    }
    public IEnumerator Shake(Vector2 _vec)
    {
        if(_vec==Vector2.zero)
            _vec=Vector2.one;
        if(vec==Vector2.zero){
            is_shaking=true;
            vec=_vec.normalized*distance;
            yield return new WaitForSeconds(_time/3);
            vec*=-1;
            yield return new WaitForSeconds(_time*2/3);
            is_shaking=false;
            vec=Vector2.zero;
        }
    }
    public IEnumerator HitStop(float stop_time,Vector3 position)
    {
        if(Random.Range(-1,1)>0)
            Destroy(Instantiate(hit_effect_1,position,Quaternion.identity),1);
        else
            Destroy(Instantiate(hit_effect_2,position,Quaternion.identity),1);
        Time.timeScale=0;
        yield return new WaitForSecondsRealtime(stop_time);
        Time.timeScale=1;
    }
}
