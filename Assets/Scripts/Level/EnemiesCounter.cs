using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private List<EnemyHP> _arrayOfBandits;
    [SerializeField] private List<ArcherHP> _arrayOfArcher;
    [SerializeField] private GameObject _collider;
    [SerializeField] private GameObject _colliderOld;
    private int countOfEnemies = 12;

    private void Start()
    {
        _collider.SetActive(false);
        countOfEnemies = PlayerPrefs.GetInt("CountEnemy", 12);  
    }

    private void Update()
    {
        if (countOfEnemies == 0)
        {
            _colliderOld.SetActive(false);
            _collider.SetActive(true);
        }
        else
        {
            foreach(var archer in _arrayOfArcher.ToArray())
                if(archer._isDie){
                    _arrayOfArcher.Remove(archer);
                    countOfEnemies--;
                    PlayerPrefs.SetInt("CountEnemy",countOfEnemies);
                    PlayerPrefs.Save();
                }
            
            foreach(var bandit in _arrayOfBandits.ToArray())
                if(bandit._isDead){
                    _arrayOfBandits.Remove(bandit);
                    countOfEnemies--;
                    PlayerPrefs.SetInt("CountEnemy",countOfEnemies);  
                    PlayerPrefs.Save();      
                }
        }
    }

    public int getCount() { return countOfEnemies; }
}
