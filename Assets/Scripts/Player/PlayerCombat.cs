using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
    private EnemyHP enemy;
    private ArcherHP archer;
    public Animator animator;
    public Transform attack_pointer;
    public int attack_damage = 40;
    public float attack_range = 0.5f;
    public LayerMask enemyLayers;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Attack();
        }
    }
    public void Attack()
    {
        int damage = 25;
        Debug.Log($"Attack! Damage: {damage}");
        enemy.TakeDamage(damage);

        archer.TakeDamage(damage);
        Debug.Log($"Attack! Damage: {damage}");
    }
    void OnDrawGizmosSelector(){
        if(attack_pointer == null) return;
        Gizmos.DrawWireSphere(attack_pointer.position, attack_range);  
    }

}
