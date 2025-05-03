using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInWasteland : MonoBehaviour
{
     [SerializeField] private List<EnemyHP> _arrayOfBandits;
    [SerializeField] private List<ArcherHP> _arrayOfArcher;
    //[SerializeField] private GameObject _collider;
    private int countOfEnemies = 14;

    private void Start()
    {
        //_collider.SetActive(false);
        countOfEnemies = PlayerPrefs.GetInt("CountEnemyInWasteland", 14);  
    }

    private void Update()
    {
       // if (countOfEnemies == 0)
            //_collider.SetActive(true);
        // else
        // {
           
        // }

         foreach(var archer in _arrayOfArcher.ToArray())
                if(archer._isDie){
                    _arrayOfArcher.Remove(archer);
                    countOfEnemies--;
                    PlayerPrefs.SetInt("CountEnemyInWasteland",countOfEnemies);
                    PlayerPrefs.Save();
                }
            
            foreach(var bandit in _arrayOfBandits.ToArray())
                if(bandit._isDead){
                    _arrayOfBandits.Remove(bandit);
                    countOfEnemies--;
                    PlayerPrefs.SetInt("CountEnemyInWasteland",countOfEnemies);  
                    PlayerPrefs.Save();      
                }
    }

    public int getCount() { return countOfEnemies; }
}
