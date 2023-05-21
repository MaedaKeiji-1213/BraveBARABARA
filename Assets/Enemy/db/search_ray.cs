using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class search_ray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public RaycastHit2D[] Search(Vector2 origin, float ray_angle, float interval_angle, float range_angle, float length, int layer = -1)
    {
        interval_angle=interval_angle<=0?1:interval_angle;
        int i = 1;
        int ray_value = (int)(range_angle / 2 / interval_angle + 1);
        RaycastHit2D[] ray_target = new RaycastHit2D[ray_value * 2 + 1];
        Vector3 vec = Vector3.zero;
        vec.x = Mathf.Cos(ray_angle * Mathf.Deg2Rad) * length;
        vec.y = Mathf.Sin(ray_angle * Mathf.Deg2Rad) * length;
        ray_target[ray_value] = (layer == -1 ? Physics2D.Raycast(origin, vec, length) : Physics2D.Raycast(origin, vec, length, layer));
        Debug.DrawRay(origin, vec * 1.1f, new Color(1, ray_target[ray_value - i - 1] != new RaycastHit2D() ? 1 : 0, 1, 1), 0.1f, false);
        while (i < ray_value)
        {
            Vector3 tmp_vec = Vector3.zero;
            tmp_vec.x = Mathf.Cos((ray_angle + interval_angle * i) * Mathf.Deg2Rad) * length;
            tmp_vec.y = Mathf.Sin((ray_angle + interval_angle * i) * Mathf.Deg2Rad) * length;
            ray_target[ray_value + i - 1] = (layer == -1 ? Physics2D.Raycast(origin, tmp_vec, length) : Physics2D.Raycast(origin, tmp_vec, length, layer));
            //Debug.DrawRay(origin, tmp_vec, new Color(1, ray_target[ray_value + i - 1] != new RaycastHit2D() ? 1 : 0, (1 - (float)i / ray_value), 1), 0.1f, false);
            //if(ray_target[ray_value+i-1].transform!=null)Debug.Log(ray_target[ray_value+i-1].transform);
            tmp_vec.x = Mathf.Cos((ray_angle - interval_angle * i) * Mathf.Deg2Rad) * length;
            tmp_vec.y = Mathf.Sin((ray_angle - interval_angle * i) * Mathf.Deg2Rad) * length;
            ray_target[ray_value - i - 1] = (layer == -1 ? Physics2D.Raycast(origin, tmp_vec, length) : Physics2D.Raycast(origin, tmp_vec, length, layer));
            //Debug.DrawRay(origin, tmp_vec, new Color((1 - (float)i / ray_value), ray_target[ray_value - i - 1] != new RaycastHit2D() ? 1 : 0, 1, 1), 0.1f, false);
            //if(ray_target[ray_value-i-1].transform!=null)Debug.Log(ray_target[ray_value-i-1].transform);
            i++;
            //Debug.Log((1-(float)i/ray_value));
        }
        return ray_target;
    }
    public RaycastHit2D[] Search_sq(Vector2 origin, float ray_angle, float interval_distance, float range_distance, float length, int layer = -1)
    {
        interval_distance=interval_distance<=0?1:interval_distance;
        int i = 1;
        int ray_value = (int)(range_distance / interval_distance + 1);
        RaycastHit2D[] ray_target = new RaycastHit2D[ray_value * 2 + 1];
        Vector3 vec = Vector3.zero;
        vec.x = Mathf.Cos(ray_angle * Mathf.Deg2Rad) * length;
        vec.y = Mathf.Sin(ray_angle * Mathf.Deg2Rad) * length;
        ray_target[ray_value] = (layer == -1 ? Physics2D.Raycast(origin, vec, length) : Physics2D.Raycast(origin, vec, length, layer));
        Debug.DrawRay(origin, vec * 1.1f, new Color(1, ray_target[ray_value - i - 1] != new RaycastHit2D() ? 1 : 0, 1, 1), 0.1f, false);
        Vector2 tmp_vec = Vector2.zero;
        tmp_vec.x = Mathf.Cos((ray_angle + 90) * Mathf.Deg2Rad);
        tmp_vec.y = Mathf.Sin((ray_angle + 90) * Mathf.Deg2Rad);
        while (i < ray_value)
        {
            ray_target[ray_value + i - 1] = (layer == -1 ? Physics2D.Raycast(origin + tmp_vec * i*interval_distance, vec, length) : Physics2D.Raycast(origin + tmp_vec * i*interval_distance, vec, length, layer));
            Debug.DrawRay(origin + tmp_vec * i*interval_distance, vec, new Color(1, ray_target[ray_value + i - 1] != new RaycastHit2D() ? 1 : 0, (1 - (float)i / ray_value), 1), 0.1f, false);
            //if(ray_target[ray_value+i-1].transform!=null)Debug.Log(ray_target[ray_value+i-1].transform);
            // ray_target[ray_value - i - 1] = (layer == -1 ? Physics2D.Raycast(origin - tmp_vec * i*interval_distance, vec, length) : Physics2D.Raycast(origin - tmp_vec * i*interval_distance, vec, length, layer));
            // Debug.DrawRay(origin - tmp_vec * i*interval_distance, vec, new Color((1 - (float)i / ray_value), ray_target[ray_value - i - 1] != new RaycastHit2D() ? 1 : 0, 1, 1), 0.1f, false);
            //if(ray_target[ray_value-i-1].transform!=null)Debug.Log(ray_target[ray_value-i-1].transform);
            i++;
            //Debug.Log((1-(float)i/ray_value));
        }
        return ray_target;
    }

}
