using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInFirstScene : MonoBehaviour
{
    [SerializeField] private List<EnemyHP> _arrayOfBandits;
    [SerializeField] private GameObject _collider;
    [SerializeField] private GameObject _colliderOld = null;

    [SerializeField] private int countOfEnemies = 5;
    [SerializeField] private int getInt = 5;

    private void Start()
    {
        _collider.SetActive(false);
        countOfEnemies = PlayerPrefs.GetInt("CountEnemyInFirstScene", getInt);  
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
            foreach(var bandit in _arrayOfBandits.ToArray())
                if(bandit._isDead){
                    _arrayOfBandits.Remove(bandit);
                    countOfEnemies--;
                    PlayerPrefs.SetInt("CountEnemyInFirstScene",countOfEnemies);  
                    PlayerPrefs.Save();      
                }
        }
    }

    public int getCount() { return countOfEnemies; }
}
