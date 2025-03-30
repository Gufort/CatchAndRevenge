using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private List<EnemyHP> _arrayOfBandits;
    [SerializeField] private List<ArcherHP> _arrayOfArcher;
    public int countOfEnemies;

    private void Start()
    {
        countOfEnemies = _arrayOfArcher.Count() + _arrayOfBandits.Count();
    }

    private void Update()
    {
        foreach(var archer in _arrayOfArcher)
            if(archer._isDie){
                _arrayOfArcher.Remove(archer);
                countOfEnemies--;
            }
        
        foreach(var bandit in _arrayOfBandits.ToArray())
            if(bandit._isDead){
                _arrayOfBandits.Remove(bandit);
                countOfEnemies--;
            }
    }
}
