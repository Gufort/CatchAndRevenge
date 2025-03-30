using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private List<EnemyHP> _arrayOfBandits;
    [SerializeField] private List<ArcherHP> _arrayOfArcher;
    [SerializeField] private GameObject _collider;
    private int countOfEnemies = 12;

    private void Start()
    {
        _collider.SetActive(false);
        countOfEnemies = PlayerPrefs.GetInt("CountEnemy", 12);  
    }

    private void Update()
    {
        foreach(var archer in _arrayOfArcher.ToArray())
            if(archer._isDie){
                _arrayOfArcher.Remove(archer);
                countOfEnemies--;
                PlayerPrefs.SetInt("CountEnemy",countOfEnemies);
                PlayerPrefs.Save();
                if(countOfEnemies == 0) _collider.SetActive(true);
            }
        
        foreach(var bandit in _arrayOfBandits.ToArray())
            if(bandit._isDead){
                _arrayOfBandits.Remove(bandit);
                countOfEnemies--;
                PlayerPrefs.SetInt("CountEnemy",countOfEnemies);  
                PlayerPrefs.Save(); 
                if(countOfEnemies == 0) _collider.SetActive(true);           
            }
        
    }

    public int getCount() { return countOfEnemies; }
}
