using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterEnemyInFirstScene : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private EnemyInFirstScene _enemiesCounter;
    void Update()
    {
       if (_count != null) _count.text = "" + _enemiesCounter.getCount(); 
    }
}
