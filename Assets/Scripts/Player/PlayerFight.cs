using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : SoundMaster
{
    [Header("Настройка характеристик персонажа: \n")]
    [SerializeField] private int damage = 25;
    [SerializeField] private float dash_dist = 2f;
    [SerializeField] private float dash_time = 0.3f;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float _delayBeforePushAway = 0.5f;
    [SerializeField] private float _pushDistance = 0.5f; 
    [SerializeField] private float _pushSpeed = 2.5f; 
    private float lastAttackTime;
    private bool is_dash = false;
    private float dash_start_time;
    public Animator animator;
    public PolygonCollider2D attack_pointer;
    private Vector2 direction;
    private bool isAttacking = false;
    private HashSet<EnemyHP> damagedBandits = new HashSet<EnemyHP>();
    private HashSet<ArcherHP> damagedArchers = new HashSet<ArcherHP>();

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
        damagedBandits.Clear();
        damagedArchers.Clear();
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

    private IEnumerator DelayBeforePushAwayAndPush(Transform Transform, Vector2 pushDirection)
    {
        yield return new WaitForSeconds(_delayBeforePushAway);
        StartCoroutine(PushAway(Transform, pushDirection));
    }

    private IEnumerator PushAway(Transform enemyTransform, Vector2 pushDirection)
    {

        float elapsedTime = 0f;

        Vector3 startPosition = enemyTransform.position;
        Vector3 targetPosition = startPosition + (Vector3)(pushDirection * _pushDistance);

        while (elapsedTime < _pushDistance / _pushSpeed)
        {
            enemyTransform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime * _pushSpeed) / _pushDistance);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        enemyTransform.position = targetPosition; 
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (isAttacking && collider2D.transform.TryGetComponent(out EnemyHP enemy))
        {
            if (!damagedBandits.Contains(enemy))
            {
                enemy.TakeDamage(damage);
                damagedBandits.Add(enemy);

                Vector2 pushDirection = (enemy.transform.position - transform.position).normalized; 
                StartCoroutine(DelayBeforePushAwayAndPush(enemy.transform, pushDirection));
            }
        }

        else if(isAttacking && collider2D.transform.TryGetComponent(out ArcherHP archer)){
            if (!damagedArchers.Contains(archer))
            {
                archer.TakeDamage(damage);
                damagedArchers.Add(archer);

                Vector2 pushDirection = (archer.transform.position - transform.position).normalized; 
                StartCoroutine(DelayBeforePushAwayAndPush(archer.transform, pushDirection));
            }
        }

        else if(isAttacking && collider2D.transform.TryGetComponent(out NarratorHP narrator)){
            narrator.TakeDamage(damage);

            Vector2 pushDirection = (narrator.transform.position - transform.position).normalized; 
            StartCoroutine(DelayBeforePushAwayAndPush(narrator.transform, pushDirection));
        }
    }
}
