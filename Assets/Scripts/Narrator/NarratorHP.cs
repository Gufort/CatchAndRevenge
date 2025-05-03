using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NarratorHP : MonoBehaviour
{
    [Header("Настройки показателей здоровья рассказчика: \n")]
    [SerializeField] private NarratorScriptableObjects _narratorSO;
    [SerializeField] private NarratorMove _narratorMove;
    [SerializeField] private CapsuleCollider2D _capsuleCollider2D;
    [SerializeField] private int _currentHP;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    public static int curr_hp_to_renderer;
    public event EventHandler OnDeath;
    private bool _canTakeDamage = true;
    private Animator _animator;
    public bool _isDie;

    private void Awake(){
        _animator = GetComponent<Animator>();
        _narratorMove = GetComponent<NarratorMove>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _currentHP = PlayerPrefs.GetInt("Narrator", _narratorSO.curr_hp);

        Debug.Log($"Narrator current HP - {_currentHP}");

        if(_currentHP <= 0){
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage){
        if(!_canTakeDamage || _isDie) return;
        
        _canTakeDamage = false;
        _animator.SetBool("TakeDamage", true);
        _currentHP -= damage;
        PlayerPrefs.SetInt("Narrator", _currentHP);
        PlayerPrefs.Save();
        
        Die();
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine(){
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("TakeDamage", false);
        _canTakeDamage = true;
    }

    private void Update()
    {
        curr_hp_to_renderer = _currentHP;
    }

    public void Die(){
        if(_currentHP <= 0){
            _capsuleCollider2D.enabled = false;
            _boxCollider2D.enabled = false;

            _narratorMove.SetDeath();
            OnDeath?.Invoke(this,EventArgs.Empty);

            _isDie = true;
            PlayerPrefs.SetInt("Narrator", 0);
            PlayerPrefs.Save();
            Debug.Log("Narrator die!");
        }
    }
}