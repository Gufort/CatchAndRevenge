using System.Collections;
using System.Collections.Generic;
using GameUtils;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamimgDistanceMin = 3f;
    [SerializeField] private float _roamimgTimerMax = 2f;

    [SerializeField] private bool _isChasing = false;
    [SerializeField] private float _chasingDistance = 4f;
    [SerializeField] private float _chasingSpeedMultiplier = 2f;

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

    private float _lastTurnTime = 0f;
    private float _turnDelay = 0.8f;

    public bool IsRunning {
        get {
            if (_navMeshAgent.velocity == Vector3.zero) {
                return false;
            } else {
                return true;
            }
        }
    }


    private enum State {Idle,Roaming,Chasing,Attacking,Death}

    private void Awake() {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;

        _roamingSpeed = _navMeshAgent.speed;
        _chasingSpeed = _navMeshAgent.speed * _chasingSpeedMultiplier;
    }


    private void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        UnityEngine.Vector3 moveDirection = new UnityEngine.Vector3(horizontal, 0, vertical);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        MovementDirectionHandler();
        StateHandler();

        if (horizontal < 0)
        {
            transform.localScale = new UnityEngine.Vector3(-1, 1, 1);
        }
        else if (horizontal > 0)
        {
            transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        }
    }


    private void StateHandler() {
        switch (_currentState) {
            case State.Roaming:
                _roamingTimer -= Time.deltaTime;
                if (_roamingTimer < 0) {
                    Roaming();
                    _roamingTimer = _roamimgTimerMax;
                }
                CheckCurrentState();
                break;
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.Attacking:
                CheckCurrentState();
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
        }
    }

    private void ChasingTarget() {
        _navMeshAgent.SetDestination(PlayerController.instance.transform.position);
        ChangeFacingDirection(transform.position, PlayerController.instance.transform.position);
    }

    public float GetRoamingAnimSpeed() {
        return _navMeshAgent.speed / _roamingSpeed;
    }

    private void CheckCurrentState() {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        State newState = State.Roaming;

        if (_isChasing) {
            if (distanceToPlayer <= _chasingDistance) {
                newState = State.Chasing;
            }
        }

        if (newState != _currentState) {
            if (newState == State.Chasing) {
                _navMeshAgent.ResetPath();
                _navMeshAgent.speed = _chasingSpeed;
            } else if (newState == State.Roaming) {
                _roamingTimer = 0f;
                _navMeshAgent.speed = _roamingSpeed;
            } else if (newState == State.Attacking) {
                _navMeshAgent.ResetPath();
            }

            _currentState = newState;
        }
    }

    private void MovementDirectionHandler() {
        if (Time.time > _nextCheckDirectionTime) {
            if (IsRunning) {
                ChangeFacingDirection(_lastPosition, transform.position);
            } else if (_currentState == State.Attacking) {
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
        return _startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(_roamimgDistanceMin, _roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) {
        if (Time.time - _lastTurnTime < _turnDelay) return; 

        Vector3 direction = targetPosition - sourcePosition;

        if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x > 0) transform.localScale = new Vector3(1, 1, 1);

        _lastTurnTime = Time.time;
    }

} 