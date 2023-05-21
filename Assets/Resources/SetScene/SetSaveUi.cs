using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class SetSaveUi : MonoBehaviour
{
    
    int index;
    // Start is called before the first frame update
    void Start()
    {
        char[] trim="save".ToCharArray();
        index=int.Parse(gameObject.name.TrimStart(trim).ToCharArray());
        SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetButton()
    {
        if(Chara_SaveManager.load(index).have_data==true){
            chara_Data sd=Chara_SaveManager.load(index);
            GetComponentInChildren<TextMeshProUGUI>().text=(sd.name+"\n"
                                                            +sd.updateTime);
            Sprite spr=Chara_SaveManager.load_ScreenShot(index);
            //Debug.Log(spr);
            GetComponent<Image>().sprite=spr;
        }
        else{

            GetComponentInChildren<TextMeshProUGUI>().text=( "save---\t--/--/-- --:--:--");
        }

    }
    public void SaveButton()
    {
        GetChildren(GameObject.Find("Parts"));
        Chara_SaveManager.chara_save(Chara_SaveData.now_data,index);
        SetButton();
    }
    public void LoadButton()
    {
        
        Debug.Log(gameObject);
        GameObject parts=GameObject.Find("Parts");
        foreach(Transform child in parts.transform){
            Destroy(child.gameObject);
        }
        Chara_SaveData.now_data=Chara_SaveManager.load(index);
        Debug.Log("patune"+Chara_SaveData.now_data.parts[0].object_name);
        //Debug.Log(Chara_SaveData.now_data.parts[0].position);
        //Debug.Log(Chara_SaveData.now_data.parts[0].angle);
        StartCoroutine(DelayCoroutine(0.5f));
        Debug.Log("chare_name"+Chara_SaveData.now_data.parts[0].object_name);
        Debug.Log("inde"+index);

        for (int i=0;Chara_SaveData.now_data.parts[i].object_name!="";i++){
            GameObject part =Resources.Load<GameObject>("SetScene/Item/"+Chara_SaveData.now_data.parts[i].object_name);
            Debug.Log("tmp:"+Chara_SaveData.now_data.parts[i].object_name);
            GameObject tmp=Instantiate(part, Chara_SaveData.now_data.parts[i].position,Quaternion.Euler(0,0,Chara_SaveData.now_data.parts[i].angle),parts.transform);
            tmp.name=tmp.name.Replace("(Clone)","");
        }
        //Destroy(transform.parent.parent.gameObject);
        //Time.timeScale=1;
    }
    public void GetChildren(GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //�q�v�f�����Ȃ���ΏI��
        if (children.childCount == 0)
        {
            return;
        }
        int n = 0;
        Chara_SaveData.now_data.parts = new partsData[128];
        foreach (Transform ob in children)
        {


            Debug.Log(ob.name);
            //Debug.Log(ob.transform.position);
            //Debug.Log(ob.transform.rotation.z);
            //Debug.Log(ob.transform.rotation.eulerAngles.z);
            Chara_SaveData.now_data.parts[n].position = ob.transform.position;
            Chara_SaveData.now_data.parts[n].object_name = ob.name;
            Chara_SaveData.now_data.parts[n].angle = ob.transform.rotation.eulerAngles.z;
            n++;
            //GetChildren(ob.gameObject);
        }
    }
    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
