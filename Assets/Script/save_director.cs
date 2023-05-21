using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
public static class SaveManager {

  // セーブデータの保存先ディレクトリ
  const string SAVE_DIRECTORY = "save";
  // セーブファイルの名前
  const string SAVE_FILE_NAME = "Equipment";
  // セーブファイルの拡張子
  const string SAVE_FILE_TAIL = ".json";

  static string path_directory;
  
  // セーブデータの一覧
  //public static Dictionary<int, SaveData> = new Dictionary<int, SaveData>();
    

  // クラス起動時にSaveファイルを読み取っておく
  
    
  public static void createDirectory(string path){
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
  }
  static SaveManager(){
    // プロジェクトディレクトリを取得    
    #if UNITY_EDITOR
      path_directory = Directory.GetCurrentDirectory();
    #else
      path_directory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
    #endif

    // セーブデータの保存先ディレクトリを取得
    path_directory +=  ("/" + SAVE_DIRECTORY + "/" );
    createDirectory(Path.GetDirectoryName(path_directory));
  }

  public static void save(SaveData sd,int index){
      sd.fileNo = index;
      sd.updateTime = DateTime.Now.ToString();//時間を文字列としてセーブデータに入れる
      if(sd.name==""){
        sd.name = "SAVE" + index.ToString();//セーブデータの名前をいれる\
      }
      
      char[] trim="Stage_".ToCharArray();
      sd.clear_stage=int.Parse(SceneManager.GetActiveScene().name.TrimStart(trim).ToCharArray());
      string json = JsonUtility.ToJson(sd);//sdをGlobalDataからjsonに変換して変数jsonに保存
      string path_file =  ( path_directory+SAVE_FILE_NAME + index.ToString() + SAVE_FILE_TAIL);//pathに参照したいファイルのディレクトリ名を追加する

      createDirectory(Path.GetDirectoryName(path_file));//ディレクトリを取得する（なかったら生成される）
      StreamWriter writer = new StreamWriter (path_file, false, Encoding.GetEncoding("UTF-8"));//パスのファイルへの書き込み用ストリームが"UTF-8"で開かれる
      writer.WriteLine(json);
      writer.Flush();
      writer.Close();
  }
  public static SaveData load(int index){
    SaveData sd=new SaveData();
    string path_file =  ( path_directory+SAVE_FILE_NAME + index.ToString() + SAVE_FILE_TAIL);//pathに参照したいファイルのディレクトリ名を追加する
    StreamReader reader=null;
    try{
      reader = new StreamReader(path_file);//読み込み用としてUTF-8形式で開く
      string json = reader.ReadToEnd();//ファイルをjsonとして内容を読み取り、jsonに入れる
      reader.Close();//読み込み用として開いたものを閉じる
      sd=JsonUtility.FromJson<SaveData>(json);
      sd.have_data=true;
      return sd;
    }
    catch{
      sd.have_data=false;
      return sd;
    }
  }
  public static void delete(int index)
  {
    string path_file =  ( path_directory+SAVE_FILE_NAME + index.ToString() + SAVE_FILE_TAIL);//pathに参照したいファイルのディレクトリ名を追加する
    File.Delete(path_file);
  }

  public static SaveData setInitValue() {
    // セーブデータを使用しない場合の初期値
    SaveData sd=new SaveData();
    sd.parts_unlock=new bool[64];
    sd.clear_stage=0;
    sd.play_time=0;
    sd.have_data=true;

    sd.updateTime = DateTime.Now.ToString();
    sd.name = "";
    return sd;
  }


}
public class save_director : MonoBehaviour
{
    
}



    /*string[] names = Directory.GetFiles(path, SAVE_FILE_NAME + "*" + SAVE_FILE_TAIL);//ディレクトリから「条件パターンのファイル名」のファイル名を取得
    //"*"は、ワイルドカードと言って　　前の文字列＋別の文字列　　を指している
    foreach (string name in names)
    {
      try
      {
        FileInfo info = new FileInfo(name);//nameのファイルを作る
        StreamReader reader = new StreamReader(info.OpenRead(), Encoding.GetEncoding("UTF-8"));//読み込み用としてUTF-8形式で開く
        string json = reader.ReadToEnd();//ファイルをjsonとして内容を読み取り、jsonに入れる
        reader.Close();//読み込み用として開いたものを閉じる
        SaveData sd = JsonUtility.FromJson<SaveData>(json);//変数jsonをJson形式からGlobalDataに変換してsdにいれる
        saveDatas.Add(sd.fileNo, sd);//読み込んだデータsdをsd.fileNoの番号のDictionaryに追加する
      }
      catch (Exception e)
      {
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//unityEditor内なら再生を止める
        #elif UNITY_STANDALONE
                    UnityEngine.Application.Quit();//unityアプリケーション内ならアプリケーションを終了
        #endif
      }
    }*/

