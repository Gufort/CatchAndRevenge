using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] EnemyScriptableObjects _enemySO;
    [SerializeField] private int curr_hp;
    public event EventHandler OnDeath;
    public static int curr_hp_to_renderer;

    [SerializeField] EnemyScript _enemy;
    [SerializeField] CapsuleCollider2D _capsuleCollider;
    [SerializeField] PolygonCollider2D _polygonCollider;

    void Awake()
    {
        _enemy = GetComponent<EnemyScript>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();

        curr_hp = PlayerPrefs.GetInt(_enemySO.enemy_name + "_curr_hp", _enemySO.enemy_curr_hp);
        curr_hp_to_renderer = curr_hp;

        Debug.Log($"Current HP - {curr_hp}");

        if(curr_hp <= 0){
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        curr_hp -= damage;
        PlayerPrefs.SetInt(_enemySO.enemy_name + "_curr_hp", curr_hp);
        PlayerPrefs.Save();
        curr_hp_to_renderer = curr_hp;
        Debug.Log("Enemy take damage!");

        Die();
    }

    private void Die()
    {   
        if (curr_hp <= 0)
        {
            _polygonCollider.enabled = false;
            _capsuleCollider.enabled = false;
            
            _enemy.SetDeath();
            OnDeath?.Invoke(this, EventArgs.Empty);
            PlayerPrefs.SetInt(_enemySO.enemy_name + "_curr_hp", 0);
            PlayerPrefs.Save();
            Debug.Log("Enemy die!");
        }
    }

    public void PolygonCollider2DOn()
    {
        _polygonCollider.enabled = true;
    }

    public void PolygonCollider2DOff()
    {
        _polygonCollider.enabled = false;
    }
}
