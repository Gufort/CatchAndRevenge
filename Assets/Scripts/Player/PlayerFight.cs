using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public Animator animator;
    public Transform attack_pointer;
    public int attack_damage = 40;
    public float attack_range = 0.5f;
    public LayerMask enemyLayers;
    private Vector2 direction;
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        if(Input.GetKeyDown(KeyCode.F)){
            Attack();
        }
    }
    void Attack(){
        animator.SetTrigger("Attack");

        Collider2D[] hit_enemies = Physics2D.OverlapCircleAll(attack_pointer.position, attack_range, enemyLayers);
    }
}
