using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GameUtils;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private State start_state;
    [SerializeField] private float roaming_dist_max = 10.0f;
    [SerializeField] private float roaming_dist_min = 3.0f;
    [SerializeField] private float roaming_timer_max = 3.0f;
    [SerializeField] private float chasing_dist = 4f;
    [SerializeField] private float chasing_speed_mult = 2f;

    private float walk_speed;
    private float chasing_speed;
    private float roaming_time;
    private NavMeshAgent navMeshAgent;
    private State curr_state;
    private UnityEngine.Vector3 roam_pos;
    private UnityEngine.Vector3 start_pos;

    private enum State { Idle, Roaming, Chasing, Attacking, Death }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        curr_state = start_state;

        walk_speed = navMeshAgent.speed;
        chasing_speed = navMeshAgent.speed * chasing_speed_mult;
    }

    void Start()
    {
        start_pos = transform.position;
        roaming_time = roaming_timer_max;
    }

    void Update()
    {
        CheckDistanceToPlayer();
        StateHandler();
    }

    private void CheckDistanceToPlayer()
    {
        float distance_to_player = UnityEngine.Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (distance_to_player <= chasing_dist)
        {
            if (curr_state != State.Chasing)
            {
                ChangeState(State.Chasing);
            }
        }
        else
        {
            if (curr_state == State.Chasing)
            {
                ChangeState(State.Roaming);
            }
        }
    }

    private void ChangeState(State new_state)
    {
        if (new_state != curr_state)
        {
            curr_state = new_state;

            if (new_state == State.Chasing)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.speed = chasing_speed;
                ChasingTarget(); // Начинаем преследование сразу
            }
            else if (new_state == State.Roaming)
            {
                roaming_time = roaming_timer_max; // Сброс таймера
                navMeshAgent.speed = walk_speed;
                Roaming(); // Начинаем бродяжничество сразу
            }
        }
    }

    private void StateHandler()
    {
        switch (curr_state)
        {
            case State.Roaming:
                roaming_time -= Time.deltaTime;
                if (roaming_time <= 0)
                {
                    Roaming(); // Обновляем позицию
                }
                break;

            case State.Chasing:
                ChasingTarget();
                break;

            case State.Attacking:
                // Логика атаки
                break;

            case State.Death:
                // Логика смерти
                break;

            default:
            case State.Idle:
                break;
        }
    }

    private void ChasingTarget(){
        navMeshAgent.SetDestination(PlayerController.instance.transform.position);
    }

    private void Roaming()
    {
        roam_pos = GetRoamingPos();
        navMeshAgent.SetDestination(roam_pos);
        roaming_time = roaming_timer_max; // Сброс таймера после перемещения
    }

    private UnityEngine.Vector3 GetRoamingPos()
    {
        return start_pos + Utils.GetRandomGir() * Random.Range(roaming_dist_min, roaming_dist_max);
    }
}
