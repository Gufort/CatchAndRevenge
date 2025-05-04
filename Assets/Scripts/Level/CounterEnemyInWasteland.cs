using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterEnemyInWasteland : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private EnemyInWasteland _enemiesCounter;
    void Update()
    {
       if (_count != null) _count.text = "" + _enemiesCounter.getCount(); 
    }
}
