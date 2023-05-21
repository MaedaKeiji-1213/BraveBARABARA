using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


[Serializable]
public struct ConfigData {
  public bool on_controller;
}

public static class Config
{
    public static ConfigData config_setting = new ConfigData();
}

public class config_data : MonoBehaviour
{
}