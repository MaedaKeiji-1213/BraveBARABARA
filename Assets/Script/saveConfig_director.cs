using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public static class SaveConfigManager {

  public static ConfigData cd;
  const string SAVE_FILE_PATH = "ConfigData.json";
  const string SAVE_DIRECTORY = "save";

  private static ConfigData setInitConfig(ConfigData cd){
    cd.on_controller=false;
    return cd;
  }

  public static void saveConfig(){
      string json = JsonUtility.ToJson (Config.config_setting);
      #if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();
      #else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
      #endif
      path +=  ("/"+SAVE_DIRECTORY +"/"+ SAVE_FILE_PATH);
      StreamWriter writer = new StreamWriter (path, false, Encoding.GetEncoding("UTF-8"));
      writer.WriteLine (json);
      writer.Flush ();
      writer.Close ();
  }

  public static void loadConfig(){
    try
    {
      #if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();
      #else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
      #endif
      FileInfo info = new FileInfo(path +"/"+SAVE_DIRECTORY + "/" + SAVE_FILE_PATH);
      StreamReader reader = new StreamReader (info.OpenRead ());
      string json = reader.ReadToEnd ();
      Config.config_setting = JsonUtility.FromJson<ConfigData>(json);
      reader.Close();//読み込み用として開いたものを閉じる
    }
    catch (Exception e)
    {
      Debug.Log("error");
      Config.config_setting =setInitConfig(new ConfigData()) ;
    }
  }
}
public class saveConfig_director : MonoBehaviour
{
    void Start(){
      SaveConfigManager.loadConfig();
      Debug.Log("ロードコンフィグ"+Config.config_setting.on_controller);
    }
    void update(){
      //SaveConfigManager.saveConfig();
    }
}