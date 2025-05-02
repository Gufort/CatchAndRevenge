using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorAttack : MonoBehaviour
{
     [Header("Настройки атаки: \n")]
    [SerializeField] private float _attackRange = 15f;
    [SerializeField] private float _attackCoolDown = 5f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private GameObject _prefabFireBoll;
    [SerializeField] private AudioClip _soundClip;
    [SerializeField] private float _spreedAngle = 30f;
    private Animator _animator;
    private bool _isAttacking = false;
    private AudioSource _audioSource;
    private float _lastShotTime = 0f;
    private NarratorMove _narratorMove;

    private void Awake()
    {
        _narratorMove = GetComponent<NarratorMove>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void StandartAttack(){
        _lastShotTime = Time.time;
        GameObject fireBoll = Instantiate(_prefabFireBoll,_attackPoint.position, Quaternion.identity);
        FireBollScript boll = fireBoll.GetComponent<FireBollScript>();

        Vector2 direction = (PlayerController.instance.transform.position - _attackPoint.position).normalized;

        _animator.SetFloat("HorizontalAttack", direction.x);
        _animator.SetFloat("VerticalAttack", direction.y);

        boll.setDirection(direction);
        _animator.SetBool("Attack", false);
    }

    private void FanAttack(){
        _lastShotTime = Time.time;
        Vector2 direction = (PlayerController.instance.transform.position - _attackPoint.position).normalized;
        for(int i = 0; i < 3; ++i){
            float angle = _spreedAngle*(i/2f - 0.5f); //угол между фаерболами
            Vector2 devDirection = Quaternion.Euler(0,0,angle) * direction;
            GameObject fireBoll = Instantiate(_prefabFireBoll,_attackPoint.position, Quaternion.identity);
            FireBollScript boll = fireBoll.GetComponent<FireBollScript>();
            boll.setDirection(devDirection);
        }
        _animator.SetBool("Attack", false);
    }

    private IEnumerator WaitForSoundAndAttack()
    {
        yield return new WaitForSeconds(0.8f);
        FanAttack();
        _isAttacking = false; 
    }

    public bool waitForAttack(){
        return _isAttacking;
    }
    public void tryAttack(){
        float distanceToPlayer = UnityEngine.Vector2.Distance(transform.position, PlayerController.instance.transform.position);
        if(distanceToPlayer <= _attackRange && Time.time >= _lastShotTime + _attackCoolDown && !_isAttacking){
            _animator.SetBool("Attack", true);
            _isAttacking = true;
            _audioSource.clip = _soundClip;
            _audioSource.Play();
            StartCoroutine(WaitForSoundAndAttack());
        }
    }

    public float getAttackRange() { return _attackRange; }
}
