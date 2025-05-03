using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NarratorAttack : MonoBehaviour
{
     [Header("Настройки атаки: \n")]
    [SerializeField] private float _attackRange = 15f;
    [SerializeField] private float _attackCoolDown = 5f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private GameObject _prefabFireBall;
    [SerializeField] private AudioClip _soundClip;
    [SerializeField] private float _spreedAngle = 30f;
    [SerializeField] private float _countOfFireBall = 9f;
    [SerializeField] private float _rangeForSplashAttack = 5f;
    [SerializeField] private NarratorScriptableObjects _narratorSO;
    private int _countOfAttack;
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
        GameObject fireBoll = Instantiate(_prefabFireBall,_attackPoint.position, Quaternion.identity);
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
            GameObject fireBoll = Instantiate(_prefabFireBall,_attackPoint.position, Quaternion.identity);
            FireBollScript boll = fireBoll.GetComponent<FireBollScript>();
            boll.setDirection(devDirection);
        }
        _animator.SetBool("Attack", false);
    }

    private void CircleAttack(){
        _lastShotTime = Time.time;
        Vector2 direction = (PlayerController.instance.transform.position - _attackPoint.position).normalized;
        for(int i = 0; i < _countOfFireBall; ++i){
            float angle = i * (360f / _countOfFireBall);
            Vector2 devDirection = Quaternion.Euler(0,0,angle) * direction;
            GameObject fireBoll = Instantiate(_prefabFireBall,_attackPoint.position, Quaternion.identity);
            FireBollScript boll = fireBoll.GetComponent<FireBollScript>();
            boll.setDirection(devDirection);
        }
        _animator.SetBool("Attack", false);
    }

    private void SplashAttack() {
        _lastShotTime = Time.time;
        Vector2 direction = (PlayerController.instance.transform.position - _attackPoint.position).normalized;

        int fireballCount = Mathf.RoundToInt(_rangeForSplashAttack / 3f);
        float angleStep = _rangeForSplashAttack / (fireballCount - 1);
        float startAngle = -_rangeForSplashAttack / 2f;
        
        for (int i = 0; i < fireballCount; ++i) {
            float currentAngle = startAngle + angleStep * i;
            Vector2 devDirection = Quaternion.Euler(0, 0, currentAngle) * direction;
            GameObject fireBall = Instantiate(_prefabFireBall, _attackPoint.position, Quaternion.identity);
            FireBollScript boll = fireBall.GetComponent<FireBollScript>();
            boll.setDirection(devDirection);
        }       

        _animator.SetBool("Attack", false);
    }

    
    private void DifferentStates(){
        if(NarratorHP.curr_hp_to_renderer > _narratorSO.max_hp * 0.8){
            if(UnityEngine.Random.Range(1,3) == 1)
                StandartAttack();
            else FanAttack();
        }
        else if(NarratorHP.curr_hp_to_renderer < _narratorSO.max_hp * 0.8 
            && NarratorHP.curr_hp_to_renderer > _narratorSO.max_hp * 0.4){
            if(UnityEngine.Random.Range(1,3) == 1)
                FanAttack();
            else CircleAttack();
            _attackCoolDown--;
        }
        else{
            int rand = UnityEngine.Random.Range(1,4);
            if(rand == 1)
                FanAttack();
            else if(rand == 2) CircleAttack();
            else SplashAttack();
            _attackCoolDown--;
        }

        if(_countOfAttack != 2){
            _attackCoolDown = 1f;
            _countOfAttack++;
        }
        else {
            _attackCoolDown = 5f;
            _countOfAttack = 0;
        }
    }

    private IEnumerator WaitForSoundAndAttack()
    {
        yield return new WaitForSeconds(0.8f);
        DifferentStates();
        _isAttacking = false; 
    }

    public bool waitForAttack()
    {
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