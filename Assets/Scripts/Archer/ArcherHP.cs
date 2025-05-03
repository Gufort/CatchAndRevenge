using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherHP : MonoBehaviour
{
    [Header("Настройки показателей здоровья лучника: \n")]
    [SerializeField] ArcherScriptableObjects _archerSO;
    [SerializeField] private int _currentHP;
    [SerializeField] ArcherMove _archerMove;
    [SerializeField] CapsuleCollider2D _capsuleCollider2D;
    [SerializeField] BoxCollider2D _boxCollider2D;
    private Animator _animator;
    public event EventHandler OnArcherDeath;
    public bool _isDie;

    private void Awake(){
        _animator = GetComponent<Animator>();
        _archerMove = GetComponent<ArcherMove>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _currentHP = PlayerPrefs.GetInt(_archerSO.archer_name + "_currentHP", _archerSO.archer_curr_hp);

        Debug.Log($"Archer current HP - {_currentHP}");

        if(_currentHP <= 0){
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage){
        _animator.SetBool("TakeDamage", true);
        _currentHP -= damage;
        PlayerPrefs.SetInt(_archerSO.archer_name + "_currentHP", _currentHP);
        PlayerPrefs.Save();
        Debug.Log("Archer take damage!");

        Die();
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("TakeDamage", false);
    }

    public void Die(){
        if(_currentHP <= 0){
            _capsuleCollider2D.enabled = false;
            _boxCollider2D.enabled = false;

            _archerMove.SetDeath();
            OnArcherDeath?.Invoke(this,EventArgs.Empty);
            _isDie = true;
            PlayerPrefs.SetInt(_archerSO.archer_name + "_currentHP", 0);
            PlayerPrefs.Save();
            Debug.Log("Archer die!");
        }
    }
}
