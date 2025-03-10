using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : SoundMaster
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float dash_dist = 2f;
    [SerializeField] private float dash_time = 0.3f;
    [SerializeField] private float attackDelay = 0.5f;
    private float lastAttackTime;
    private bool is_dash = false;
    private float dash_start_time;
    public Animator animator;
    public PolygonCollider2D attack_pointer;
    private Vector2 direction;
    private bool isAttacking = false;
    private HashSet<EnemyHP> damagedEnemies = new HashSet<EnemyHP>();

    void Start()
    {
        ColliderOff();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);


        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackDelay)
        {
            PlaySound(sounds[0], volume: 0.4f, loop: false, p1: 0.5f, p2: 0.7f);
            Attack();
            lastAttackTime = Time.time;
        }
        if (is_dash) Dash();
    }

    void Attack()
    {
        isAttacking = true;
        damagedEnemies.Clear();
        ColliderOn();
        is_dash = true;
        dash_start_time = Time.time;
        animator.SetTrigger("Attack");
        Invoke("ColliderOff", 0.5f);
    }

    public void ColliderOff()
    {
        attack_pointer.enabled = false;
        isAttacking = false;
    }

    private void ColliderOn()
    {
        attack_pointer.enabled = true;
    }

    private void Dash()
    {
        float eltime = Time.time - dash_start_time;

        if (eltime < dash_time)
        {
            Vector2 dash_direction = direction.normalized;
            transform.position += (Vector3)(dash_direction * (dash_dist / dash_time) * Time.deltaTime);
        }
        else
        {
            is_dash = false;
            ColliderOff();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (isAttacking && collider2D.transform.TryGetComponent(out EnemyHP enemy))
        {
            if (!damagedEnemies.Contains(enemy))
            {
                enemy.TakeDamage(damage);
                damagedEnemies.Add(enemy);
            }
        }
    }
}
