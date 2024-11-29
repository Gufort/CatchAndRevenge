using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyScriptableObjects : ScriptableObject
{
    public string enemy_name;
    public int enemy_curr_hp;
    public bool isDead = false;
}
