using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameUtils;
using System;

public class LittleBoyMove : MonoBehaviour
{
    [Header("Настройки передвижения мальчика: \n")]
    [SerializeField] private DialogueManager _dm;
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private State _startingState;
    [SerializeField] private bool _isChasing = false;
    [SerializeField] private float _chasingSpeedMultiplier = 2f;
    [SerializeField] private float _chasingDistance = 10f;
    [SerializeField] private float _maxDistanceToPlayer = 4f;
    public bool isDialogueEnded;


    private Animator animator;
    private NavMeshAgent _navMeshAgent;
    private State _currentState;

    private float _chasingSpeed;


    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;
    private enum State { Idle,
    Chasing}

    private void Awake() {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;

        _chasingSpeed = _navMeshAgent.speed * _chasingSpeedMultiplier;
    }

    private void Update() {
        isDialogueEnded = (_dm.isTrueEnd && _dialogueTrigger.alreadyTriggered);
        CheckCurrentState();
        MovementDirectionHandler();
        StateHandler();

        Vector3 velocity = _navMeshAgent.velocity;

        float horizontal = velocity.x / _chasingSpeed; 
        float vertical = velocity.y / _chasingSpeed;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("IsRunning", IsRunning);
    }

    private void StateHandler() {
        switch (_currentState) {
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;

            case State.Idle:
                CheckCurrentState();
                break;
        }
    }
    private void ChasingTarget()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        if (distanceToPlayer <= _chasingDistance && distanceToPlayer > _maxDistanceToPlayer)
            _navMeshAgent.SetDestination(playerPosition);
        else
            _navMeshAgent.ResetPath();

        ChangeFacingDirection(transform.position, playerPosition);
    }

    private void CheckCurrentState() {
        if(isDialogueEnded){
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            State newState = (distanceToPlayer <= _chasingDistance) ? State.Chasing : State.Idle;

            if (newState != _currentState)
            {
                _currentState = newState;
                if (_currentState == State.Chasing)
                    _navMeshAgent.speed = _chasingSpeed;
                else
                    _navMeshAgent.speed = _chasingSpeed / _chasingSpeedMultiplier;
            }
        }
    }

    private void MovementDirectionHandler() {
        if (Time.time > _nextCheckDirectionTime) {
            if (IsRunning) {
                ChangeFacingDirection(_lastPosition, transform.position);
            } 
            _lastPosition = transform.position;
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
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
