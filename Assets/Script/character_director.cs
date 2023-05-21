using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public static class Chara_SaveManager
{

    // ?Z?[?u?f?[?^??????f?B???N?g??
    const string SAVE_DIRECTORY = "save";
    // ?Z?[?u?t?@?C??????O
    const string SAVE_FILE_NAME = "save";
    // ?Z?[?u?t?@?C????g???q
    const string SAVE_FILE_TAIL = ".json";

     public static string path_directory;

    // ?Z?[?u?f?[?^???
    //public static Dictionary<int, SaveData> = new Dictionary<int, SaveData>();


    // ?N???X?N??????Save?t?@?C?????????????


    public static void createDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    static Chara_SaveManager()
    {
        // ?v???W?F?N?g?f?B???N?g?????��    
        #if UNITY_EDITOR
                path_directory = Directory.GetCurrentDirectory();
        #else
            path_directory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
        #endif

        // ?Z?[?u?f?[?^??????f?B???N?g?????��
        path_directory += ("/" + SAVE_DIRECTORY + "/");
        createDirectory(Path.GetDirectoryName(path_directory));
    }

    public static void chara_save(chara_Data sd, int index)
    {
        sd.updateTime = DateTime.Now.ToString();//????????????Z?[?u?f?[?^??????
        if(sd.name==""){
            sd.name = "SAVE" + index.ToString();//?Z?[?u?f?[?^????O???????
        }
        CameraScreenShotCapturer cs;
        cs = GameObject.Find("Camera").GetComponent<CameraScreenShotCapturer>();
        sd.fileNo = index;
        
        string json = JsonUtility.ToJson(sd);//sd??GlobalData????json??????????json????
        string path_file = (path_directory + SAVE_FILE_NAME + index.ToString() + SAVE_FILE_TAIL);//path??Q????????t?@?C????f?B???N?g????????????

        createDirectory(Path.GetDirectoryName(path_file));//?f?B???N?g?????��????i????????�i???????j[
        cs.CaptureScreenShot(path_directory + SAVE_FILE_NAME + index.ToString() + ".png");
        StreamWriter writer = new StreamWriter(path_file, false, Encoding.GetEncoding("UTF-8"));//?p?X??t?@?C????????????p?X?g???[????"UTF-8"??J?????
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }
    public static chara_Data load(int index)
    {
        chara_Data sd = new chara_Data();
        string path_file = (path_directory + SAVE_FILE_NAME + index.ToString() + SAVE_FILE_TAIL);//path??Q????????t?@?C????f?B???N?g????????????
        StreamReader reader = null;
        try
        {
            reader = new StreamReader(path_file);//??????p?????UTF-8?`????J??
            string json = reader.ReadToEnd();//?t?@?C????json???????e??????Ajson??????
            reader.Close();//??????p?????J?????????????
            sd = JsonUtility.FromJson<chara_Data>(json);
            //Debug.Log("load"+index);
            sd.have_data = true;
            return sd;
        }
        catch
        {
            sd.have_data = false;
            return sd;
        }
    }
    public static Sprite load_ScreenShot(int index)
    {
        string path_file = (path_directory + SAVE_FILE_NAME + index.ToString() + ".png");
        Sprite ss=null;
        bool is_end=false;
        //IEnumerator Start()
	    {   
            //Debug.Log (path_file);

            byte[] bytes = File.ReadAllBytes(path_file);
            Texture2D texture = new Texture2D(200, 200);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);
            Rect rect = new Rect(0f, 0f, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);
            ss=sprite;

            //yield return null;
	    }
        return ss;
    }

    

    public static void delete(int index)
    {
        string path_file = (path_directory + SAVE_FILE_NAME + index.ToString() + SAVE_FILE_TAIL);//path??Q????????t?@?C????f?B???N?g????????????
        File.Delete(path_file);
    }

    public static chara_Data setInitValue()
    {
        // ?Z?[?u?f?[?^???g?p?????????????l
        chara_Data sd = new chara_Data();
        sd.have_data=true;
        sd.parts=new partsData[128];
        sd.parts[0].object_name="head";
        sd.parts[0].position=Vector2.zero;
        sd.parts[0].angle=0;
        sd.object_name = "";
        
        return sd;
    }



}
public class character_director : MonoBehaviour
{

}