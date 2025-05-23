using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObjects _enemySO;
    [SerializeField] private int curr_hp;

    [SerializeField] private EnemyScript _enemy;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private GameObject _attackZone;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private PolygonCollider2D _polygonCollider;
    private NavMeshAgent _navMeshAgent;
    public event EventHandler OnDeath;
    public static int curr_hp_to_renderer;
    public bool _isDead = false;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<EnemyScript>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _polygonCollider = _attackZone.GetComponent<PolygonCollider2D>();
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
        _animator.SetBool("TakeDamage", true);
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("TakeDamage", false);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out PlayerController player) && 
            !_isDead && 
            other.IsTouching(_polygonCollider))
        {
            player.TakeDamage(transform, _enemySO.damage);
        }
    }
}
