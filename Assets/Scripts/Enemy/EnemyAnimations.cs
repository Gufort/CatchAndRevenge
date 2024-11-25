using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private EnemyScript enemy;
    private Animator animator;
    private string IS_RUNNING = "IsRunning";
    private string CHASING_SPEED = "ChasingSpeed";

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Update(){
        animator.SetBool(IS_RUNNING, enemy.IsRunning);
        animator.SetFloat(CHASING_SPEED, enemy.GetRoamingAnimSpeed());
            
    }
}
