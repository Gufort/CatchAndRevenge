using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArcherScriptableObjects : ScriptableObject
{
    public int damage = 10;
    public string archer_name;
    public int archer_curr_hp = 100;
}
