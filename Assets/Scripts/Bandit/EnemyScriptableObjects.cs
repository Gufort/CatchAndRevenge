using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyScriptableObjects : ScriptableObject
{
    public int damage = 10;
    public string enemy_name;
    public int enemy_curr_hp = 100;
}
