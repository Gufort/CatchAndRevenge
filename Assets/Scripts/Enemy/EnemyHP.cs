using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int max_hp;
    [SerializeField] private int curr_hp;
    public static int curr_hp_to_renderer;

    void Start()
    {
        curr_hp = max_hp;
        curr_hp_to_renderer = curr_hp;
    }

    public void TakeDamage(int damage)
    {
        if (curr_hp <= 0) return;

        curr_hp -= damage;
        curr_hp_to_renderer = curr_hp;
        Debug.Log("Enemy take damage!");

        if (curr_hp <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy die!");
    }
}
