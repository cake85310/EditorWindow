using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int id;
    public string name;
    public string description;
    public string creation;
    public string modification;
}



[CreateAssetMenu(fileName = "New Data",menuName ="Create Data Asset",order=1)]
public class DataTest : ScriptableObject
{
    public Data data;
}
