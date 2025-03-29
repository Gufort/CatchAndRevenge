using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] EnemyScriptableObjects _enemySO;
    [SerializeField] private int curr_hp;

    [SerializeField] EnemyScript _enemy;
    [SerializeField] CapsuleCollider2D _capsuleCollider;
    [SerializeField] PolygonCollider2D _polygonCollider;
    [SerializeField] BoxCollider2D _boxCollider2D;
    private NavMeshAgent _navMeshAgent;
    public event EventHandler OnDeath;
    public static int curr_hp_to_renderer;
    private bool _isDead = false;

    void Awake()
    {
        _enemy = GetComponent<EnemyScript>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
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
            _isDead = true;
            _polygonCollider.enabled = false;
            _capsuleCollider.enabled = false;
            _boxCollider2D.enabled = false;
            
            _enemy.SetDeath();
            OnDeath?.Invoke(this, EventArgs.Empty);
            PlayerPrefs.SetInt(_enemySO.enemy_name + "_curr_hp", 0);
            PlayerPrefs.Save();
            _navMeshAgent.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform.TryGetComponent(out PlayerController player) && !_isDead){
            player.TakeDamage(transform, _enemySO.damage);
            Vector3 playerPosition = PlayerController.instance.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;

            UnityEngine.Vector2 pushDirection = direction; 
            player.transform.position += (UnityEngine.Vector3)(pushDirection * 0.1f);
        }
    }
}
