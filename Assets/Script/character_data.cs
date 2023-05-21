using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]

public struct partsData
{
    public string object_name;
    public Vector2 position;
    public float angle;
}

public struct chara_Data
{
    /*�ۑ��f�[�^*/
    public bool have_data; 
    public partsData[] parts;

    public string name;
    public string object_name;
    public string updateTime;
    public int fileNo;//�t�@�C���ԍ�
}

public static class Chara_SaveData
{
    public static chara_Data now_data = new chara_Data();
}

public class chara_data : MonoBehaviour
{
    
}