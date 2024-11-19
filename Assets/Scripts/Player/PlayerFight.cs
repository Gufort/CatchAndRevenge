using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : SoundMaster
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float dash_dist = 2f;
    [SerializeField] private float dash_time = 0.3f;
    private bool is_dash = false;
    private float dash_start_time;
    public Animator animator;
    public PolygonCollider2D attack_pointer;
    private Vector2 direction;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySound(sounds[0], volume: 0.4f, loop: false, p1: 0.5f, p2: 0.7f);
            Attack();
        }

        if (is_dash) Dash();
    }

    void Attack()
    {
        ColliderOn();
        is_dash = true;
        dash_start_time = Time.time;
        animator.SetTrigger("Attack");
        ColliderOn();
        Invoke("ColliderOff", 0.5f);
    }

    public void ColliderOff()
    {
        attack_pointer.enabled = false;
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
        if (collider2D.transform.TryGetComponent(out EnemyHP enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}

