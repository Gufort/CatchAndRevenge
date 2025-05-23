using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameUtils;
using System;

public class NarratorMove : MonoBehaviour
{
    [Header("Настройки передвижения рассказчика: \n")]
    [SerializeField] private DialogueManager _dm;
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private State _startingState;
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimerMax = 2f;
    [SerializeField] private bool _isChasing = false;
    [SerializeField] private float _chasingSpeedMultiplier = 2f;
    [SerializeField] private float _chasingDistance = 10f;
    [SerializeField] private float _maxDistanceToPlayer = 4f;
    public bool isDialogueEnded;

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

    [SerializeField] private bool _isAttacking;
    private NarratorAttack narratorAttack;

    public bool IsRunning {
        get {
            return _navMeshAgent.velocity != Vector3.zero;
        }
        set { IsRunning = value; }
    }

    private enum State { Idle,
    Roaming,
    Chasing,
    Attack,
    Death }

    private void Awake() {
        narratorAttack = GetComponent<NarratorAttack>();
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;

        _roamingSpeed = _navMeshAgent.speed;
        _chasingSpeed = _navMeshAgent.speed * _chasingSpeedMultiplier;
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

        if(IsRunning){
            animator.SetBool("IsRunning", true);
        }
        if(vertical == 0 && horizontal == 0) {
            animator.SetBool("IsRunning", false);
        }
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

            case State.Attack:
                narratorAttack.tryAttack();
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

    public void setDeath(){
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
    }


    private void CheckCurrentState() {
        if(isDialogueEnded){
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            State newState = _currentState;

            if (_isAttacking && distanceToPlayer <= narratorAttack.getAttackRange()) {
                newState = State.Attack;
            }
            else if (_isChasing && distanceToPlayer <= _chasingDistance && !narratorAttack.waitForAttack()) {
                newState = State.Chasing;
            }
            else if (newState != State.Idle) {
                newState = State.Roaming;
            }

            if (newState != _currentState) {
                if (newState == State.Chasing) {
                    _navMeshAgent.ResetPath();
                    _navMeshAgent.speed = _chasingSpeed;
                } 
                else if (newState == State.Roaming) {
                    _roamingTimer = 0f;
                    _navMeshAgent.speed = _roamingSpeed;
                } 
                else if (newState == State.Attack) {
                    _navMeshAgent.ResetPath();
                }
                _currentState = newState;
            }
        }
    }

    private void MovementDirectionHandler() {
        if (Time.time > _nextCheckDirectionTime) {
            if (IsRunning) {
                ChangeFacingDirection(_lastPosition, transform.position);
            } 
             else if (_currentState == State.Attack) {
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
