using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public bool twoPerson = false;
    public Sprite sprite;
    public Sprite spriteSecond;
    public int[] order;
    [TextArea(3, 10)]
    public string[] sentences;
}
