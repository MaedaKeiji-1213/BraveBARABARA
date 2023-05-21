using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    FadeManager fm;
    // Start is called before the first frame update
    void Start()
    {
        fm=FadeManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void retry_button(){
        fm.LoadScene(SceneManager.GetActiveScene().name,1);
    }
    public void back_button(){
        fm.LoadScene("SetScene",1);
    }
    public void end(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }
}
