using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    FadeManager fade_manager;
    public GameObject lock_object=null;

    // Start is called before the first frame update
    void Awake()
    {
        
        fade_manager = FadeManager.Instance;
        if(!GameData.now_data.have_data)
            GameData.now_data=SaveManager.setInitValue();
        if(!Chara_SaveData.now_data.have_data   )
            Chara_SaveData.now_data=Chara_SaveManager .setInitValue();
        if(lock_object!=null){
            char[] trim="Stage_".ToCharArray();
            int index= int.Parse(gameObject.name.TrimStart(trim).ToCharArray());
            if(GameData.now_data.clear_stage>=index-1){
                lock_object.SetActive(false);
                GetComponent<Button>().enabled=true;
            }
            else {
                lock_object.SetActive(true);
                GetComponent<Button>().enabled=false;
            }
        }
    }

    public void ChangeTitleScene()
    {
        fade_manager.LoadScene("Title", 1.0f);
    }
    public void ChangeStageSelectScene()
    {
        fade_manager.LoadScene("StageSelectScene", 1.0f);
    }

    public void ChangeSetScene()
    {
        fade_manager.LoadScene("SetScene", 1.0f);
    }
    public void ChangeGameScene()
    {
        GetChildren(GameObject.Find("Parts").gameObject);
        fade_manager.LoadScene(SavePoint.stage_name, 1.0f);

    }
    public void SelectStage()
    {
        SavePoint.respone_point=0;
        SavePoint.stage_name=gameObject.name;
        FadeManager.Instance.LoadScene("SetScene", 1.0f);
    }
    public void GetChildren(GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //?q?v?f??????????I??
        if (children.childCount == 0)
        {
            return;
        }
        int n = 0;
        Chara_SaveData.now_data.parts = new partsData[128];
        foreach (Transform ob in children)
        {
            Debug.Log(ob.name);
            Debug.Log(ob.transform.position);
            Debug.Log(ob.transform.rotation.eulerAngles.z);
            Chara_SaveData.now_data.parts[n].object_name = ob.name;
            Chara_SaveData.now_data.parts[n].position = ob.transform.position;
            Chara_SaveData.now_data.parts[n].angle = ob.transform.rotation.eulerAngles.z;
            n++;
            //GetChildren(ob.gameObject);
        }
    }
}
