using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int max_hp;
    private int curr_hp;

    void Start(){
        curr_hp = max_hp;
    }

    public void TakeDamage(int damage){
        curr_hp -= damage;
        Debug.Log("Enemy take damage!");
        Die();
    }

    public void Die(){
        if(curr_hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy die!");
        }
    }
}
