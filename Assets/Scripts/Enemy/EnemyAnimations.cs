using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private EnemyScript _enemy1;
    [SerializeField] private EnemyHP _enemy2;
    private Animator animator;
    private string IS_RUNNING = "IsRunning";
    private string CHASING_SPEED = "ChasingSpeed";
    private string ATTACK = "Attack";
    private string IS_DIE = "IsDie";

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start(){
        _enemy1.OnAttack += _enemy1_OnAttack;
        _enemy2.OnDeath += enemy2_OnDeath;
    }

    void OnDestroy(){
        _enemy1.OnAttack -= _enemy1_OnAttack;
    }

    void Update(){
        animator.SetBool(IS_RUNNING, _enemy1.IsRunning);
        animator.SetFloat(CHASING_SPEED, _enemy1.GetRoamingAnimSpeed());
            
    }

    public void TriggerAttackAnimationsOn(){
        _enemy2.PolygonCollider2DOn();
    }

    public void TriggerAttackAnimationsOff(){
        _enemy2.PolygonCollider2DOff();
    }

    private void _enemy1_OnAttack(object sender, System.EventArgs e){
        animator.SetTrigger(ATTACK);
    }

    private void enemy2_OnDeath(object sender, System.EventArgs e){
        animator.SetBool(IS_DIE, true);
    }
}
