
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public struct SaveData {
  /*保存データ*/
  public bool have_data;

  /*ワールドデータ*/
  public bool[] parts_unlock;

  /*必須データ*/
  public int clear_stage;
  public string save_point;

  public uint play_time;
  public string updateTime;//時間
  public string name;//ファイル名
  public int fileNo;//ファイル番号
}

public static class GameData
{
    public static SaveData now_data = new SaveData();
}

public class save_data :MonoBehaviour
{
}