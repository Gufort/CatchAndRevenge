using System;
using System.Collections;
using System.Collections.Generic;
using GameUtils;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Настройки характеристик бандита: \n")]
    [SerializeField] private DialogueManager _dm;
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private State _startingState;
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimerMax = 2f;
    [SerializeField] private float _ChasingDist = 4f;
    [SerializeField] private bool _isChasing = false;
    [SerializeField] private float _chasingSpeedMultiplier = 2f;

    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private float _attackingDistance = 1f;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private float _maxDistanceToPlayer = 1f;
    private PlayerController _player;
    private float _nextTimeAttack = 0f;
    private float _chasingDistance;

    private Animator animator;
    private NavMeshAgent _navMeshAgent;
    private State _currentState;
    private float _roamingTimer;
    private Vector3 _roamPosition;
    private Vector3 _startingPosition;

    private float _roamingSpeed;
    private float _chasingSpeed;

    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

    public event EventHandler OnAttack;

    public bool isDialogueEnded;

    public bool IsRunning {
        get {
            return _navMeshAgent.velocity != Vector3.zero;
        }
        set { IsRunning = value; }
    }

    private enum State { Idle, Roaming, Chasing, Attacking, Death }

    private void Awake() {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;

        _chasingDistance = _ChasingDist;

        _roamingSpeed = _navMeshAgent.speed;
        _chasingSpeed = _navMeshAgent.speed * _chasingSpeedMultiplier;
    }

    private void Start()
    {
        _player = PlayerController.instance;
    }

    private void Update() {
        isDialogueEnded = (_dm.isTrueEnd && _dialogueTrigger.alreadyTriggered);
       

        MovementDirectionHandler();
        StateHandler();

        Vector3 velocity = _navMeshAgent.velocity;

        float horizontal = velocity.x / _roamingSpeed; 
        float vertical = velocity.y / _roamingSpeed;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    public void SetDeath(){
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
    }

    private void StateHandler() {
        switch (_currentState) {
            case State.Roaming:
                _roamingTimer -= Time.deltaTime;
                if (_roamingTimer < 0) {
                    Roaming();
                    _roamingTimer = _roamingTimerMax;
                }
                CheckCurrentState();
                break;
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
            case State.Death:
                break;
            case State.Idle:
                CheckCurrentState();
                break;
        }
    }

    public float GetRoamingAnimSpeed() {
        return _navMeshAgent.speed / _roamingSpeed;
    }

    private void ChasingTarget() {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer > _maxDistanceToPlayer) {
            _navMeshAgent.SetDestination(playerPosition); 
        } else {
            _navMeshAgent.ResetPath();
        }

        ChangeFacingDirection(transform.position, playerPosition);
    }

    private void CheckCurrentState() {
        if(isDialogueEnded){
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            State newState = _currentState;

            if (_isAttacking && distanceToPlayer <= _attackingDistance) {
                newState = State.Attacking;
            }
            else if (_isChasing && distanceToPlayer <= _chasingDistance) {
                newState = State.Chasing;
            }
            else if (distanceToPlayer > _chasingDistance) {
                newState = State.Roaming;
            }

            if (newState != _currentState) {
                switch (newState) {
                    case State.Chasing:
                        _navMeshAgent.ResetPath();
                        _navMeshAgent.speed = _chasingSpeed;
                        break;
                    case State.Roaming:
                        _roamingTimer = 0f;
                        _navMeshAgent.speed = _roamingSpeed;
                        break;
                    case State.Attacking:
                        _navMeshAgent.ResetPath();
                        ChangeFacingDirection(transform.position, PlayerController.instance.transform.position);
                        break;
                }
                _currentState = newState;
            }
        }
        
    }

    private void AttackingTarget() {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        if (distanceToPlayer > _attackingDistance) {
            _currentState = State.Chasing;
            return;
        }

        if (Time.time > _nextTimeAttack) {
            OnAttack?.Invoke(this, EventArgs.Empty);
            Vector3 playerPosition = PlayerController.instance.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;

            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            _nextTimeAttack = Time.time + _attackRate;
        }
    }

    private void MovementDirectionHandler() {
        if (Time.time > _nextCheckDirectionTime) {
            if (IsRunning) {
                ChangeFacingDirection(_lastPosition, transform.position);
            } 
            else if (_currentState == State.Attacking) {
                ChangeFacingDirection(transform.position, PlayerController.instance.transform.position);
            }

            _lastPosition = transform.position;
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
    }

    private void Roaming() {
        _startingPosition = transform.position;
        _roamPosition = GetRoamingPosition();

        if (_navMeshAgent.isActiveAndEnabled && !_navMeshAgent.isStopped) {
            _navMeshAgent.SetDestination(_roamPosition);
        }
    }

    private Vector3 GetRoamingPosition() {
        return _startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) {
        Vector3 direction = (targetPosition - sourcePosition).normalized;

        if (direction.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}