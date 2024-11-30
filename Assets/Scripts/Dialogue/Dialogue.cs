using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public bool twoPerson = false;
    public string name;
    public Sprite sprite;
    public string nameSecond = "";
    public Sprite spriteSecond;
    [TextArea(3, 10)]
    public string[] sentences;
}
