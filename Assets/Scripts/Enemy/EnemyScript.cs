using System.Collections;
using System.Collections.Generic;
using GameUtils;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private State start_state;
    [SerializeField] private float roaming_dist_max = 10.0f;
    [SerializeField] private float roaming_dist_min = 3.0f;
    [SerializeField] private float roaming_timer_max = 3.0f;
    private float roaming_time;
    private NavMeshAgent navMeshAgent;
    private State curr_state;
    private UnityEngine.Vector3 roam_pos;
    private UnityEngine.Vector3 start_pos;

    private enum State{Idle,Roaming}//State
    

    private void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        curr_state = start_state;
    }

    void Start(){
        start_pos = transform.position;
        roaming_time = roaming_timer_max;
    }

    void Update(){
        switch(curr_state){
            case State.Idle:
                break;
            case State.Roaming:
                roaming_time -= Time.deltaTime;
                if(roaming_time<0){
                    roaming_time = roaming_dist_max;
                    Roaming();
                }
                break;
        }
    }

    private void Roaming(){
        roam_pos = GetRoamingPos();
        navMeshAgent.SetDestination(roam_pos);
    }

    private UnityEngine.Vector3 GetRoamingPos(){
        return start_pos + Utils.GetRandomGir() * Random.Range(roaming_dist_min, roaming_dist_max);
    }
}
