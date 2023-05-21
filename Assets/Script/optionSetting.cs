using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionSetting : MonoBehaviour
{
    [SerializeField] GameObject main_scene;
    [SerializeField] GameObject setting_scene;
    // Start is called before the first frame update
    void Start()
    {

        main_scene.SetActive(true);
        setting_scene.SetActive(false);
    }
    public void SettingScene()
    {
        main_scene.SetActive(false);
        setting_scene.SetActive(true);
    }
    public void ReturnScene()
    {
        main_scene.SetActive(true);
        setting_scene.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {  
    }
}
