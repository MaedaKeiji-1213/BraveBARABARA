using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;
public class set_saveUI : MonoBehaviour
{
    int index;
    [SerializeField] bool active_when_not;
    [SerializeField] GameObject sub_menu;
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
        if(SaveManager.load(index).have_data==true){
            SaveData sd=SaveManager.load(index);
            GetComponentInChildren<TextMeshProUGUI>().text=(sd.name+"\n"
                                           +"now stage : "+sd.clear_stage
                                           +"\tplay time : "+new TimeSpan(0,0,(int)sd.play_time).ToString(@"hh\:mm\:ss")
                                           +"\t"+sd.updateTime);
        }
        else{
            GetComponentInChildren<TextMeshProUGUI>().text=(  "save---\n"
                                           +"name :---     level:--- \n"
                                           +"now stage :---    play time :--:--:--           --/--/-- --:--:--");
            if(active_when_not==false)                               
                GetComponent<Button>().interactable=false;
        }

    }

    public void SaveButton()
    {
        Chara_SaveManager.chara_save(Chara_SaveData.now_data,index);
        SetButton();
    }
    
    public void LoadButton()
    {
        GameData.now_data=SaveManager.load(index);
        Destroy(transform.parent.parent.parent.gameObject);
        Time.timeScale=1;
    }

    public void OpenSubMenu()
    {
        if(Input.GetMouseButtonUp(1)&&GetComponent<Button>().interactable==true){
            Instantiate(sub_menu,Input.mousePosition,Quaternion.identity,transform).name="SubMenu"+Regex.Replace(gameObject.name, @"[^0-9]", "");
        }
    }

}
