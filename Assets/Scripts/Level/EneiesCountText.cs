using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EneiesCountText : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private EnemiesCounter _enemiesCounter;
    void Update()
    {
       if (_count != null) _count.text = "Кол-во врагов: " + _enemiesCounter.getCount(); 
    }
}
