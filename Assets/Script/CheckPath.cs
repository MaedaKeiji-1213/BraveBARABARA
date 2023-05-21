using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetFullPath(transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	string GetFullPath(Transform t)
	{
		string path = t.name;
		var parent = t.parent;
		while (parent)
		{
			path = $"{parent.name}/{path}";
			parent = parent.parent;
		}
		return path;
	}
}
