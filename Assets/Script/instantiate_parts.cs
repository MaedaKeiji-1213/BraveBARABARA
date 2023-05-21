using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SavePoint
{
    public static string stage_name = "Stage_1";
    public static short respone_point = 0;
}

public class instantiate_parts : MonoBehaviour
{
    [SerializeField] GameObject go;
    Vector2 head_pos;
    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log(Chara_SaveData.now_data.parts[0].object_name);
        Debug.Log(Chara_SaveData.now_data.parts[0].position);
        Debug.Log(Chara_SaveData.now_data.parts[0].angle);*/

        transform.position = GameObject.Find("SavePoint" + SavePoint.respone_point.ToString()).transform.position;
        for (int i = 0; Chara_SaveData.now_data.parts[i].object_name != null; i++)
        {
            if(Chara_SaveData.now_data.parts[i].object_name=="head"){
                head_pos=Chara_SaveData.now_data.parts[i].position;
            }
        }
        for (int i = 0; Chara_SaveData.now_data.parts[i].object_name != null; i++)
        {
            GameObject part = Resources.Load<GameObject>("Parts/" + Chara_SaveData.now_data.parts[i].object_name);
            GameObject tmp = Instantiate(part,(Vector2)transform.position+Chara_SaveData.now_data.parts[i].position-head_pos, Quaternion.Euler(0, 0, Chara_SaveData.now_data.parts[i].angle), transform);
            tmp.name = tmp.name.Replace("(Clone)", "");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
