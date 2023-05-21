using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] Transform pallet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PartsLeftScene()
    {
        if(pallet.transform.position.x!= 0)
        {
            pallet.transform.position += (new Vector3(30, 0, 0));
        }
        else
        {
            pallet.transform.position += (new Vector3(-30, 0, 0));
        }
    }

    public void PartsRightScene()
    {
        if (pallet.transform.position.x != -30)
        {
            pallet.transform.position+=(new Vector3(-30, 0, 0));
        }
        else
        {
            pallet.transform.position += (new Vector3(30, 0, 0));
        }
            
    }
}
