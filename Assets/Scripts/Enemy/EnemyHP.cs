using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int max_hp;
    private int curr_hp;
    public static int curr_hp_to_renderer;

    void Start(){
        curr_hp = max_hp;
        curr_hp_to_renderer = curr_hp;
    }

    public void TakeDamage(int damage){
        curr_hp -= damage;
        curr_hp_to_renderer = curr_hp;
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
