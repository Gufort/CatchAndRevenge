using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : SoundMaster
{
    [SerializeField] private int max_hp;
    [SerializeField] private float _damageRecoveryTime = 0.5f;
    public bool _canTakeDamage;
    private PlayerDeath _playerDeath;
    static public int curr_hp_to_renderer;

    public static PlayerController instance{get;private set;}
    public float speed;
    public Animator animator;
    private UnityEngine.Vector2 direction;
    private Rigidbody2D rb;
    private bool isMoving = false;
    public int curr_hp = 100;
    public VectorValue pos;
    public Transform attackCollider;
    public bool canMove = true;
    private UnityEngine.Vector2 _lastDirection;

    private void Awake() {
        curr_hp = 100;
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        _playerDeath = GetComponent<PlayerDeath>();
    }

    void Start()
    {
        _canTakeDamage = true;
        transform.position = pos.initialValue;
        curr_hp = max_hp;
    }

    void Update()
    {
        if (canMove)
        {
            curr_hp_to_renderer = curr_hp;
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);

            if (direction.sqrMagnitude > 0)
            {
                _lastDirection = direction.normalized;
                if (!isMoving)
                {
                    PlaySound(sounds[0], volume: 0.4f, loop: true, p1: 0.9f, p2: 1f);
                    isMoving = true;
                }
            }

            else
            {
                if (isMoving)
                {
                    StopSound();
                    isMoving = false;
                }
            }

            if (direction.y > 0)
            {
                attackCollider.rotation = UnityEngine.Quaternion.Euler(0, 0, 90);
                attackCollider.position = new UnityEngine.Vector2(transform.position.x, transform.position.y + 1f);
            }
            else if (direction.y < 0)
            {
                attackCollider.rotation = UnityEngine.Quaternion.Euler(0, 0, -90);
                attackCollider.position = new UnityEngine.Vector2(transform.position.x, transform.position.y - 1f);
            }
            else if (direction.x < 0)
            {
                attackCollider.rotation = UnityEngine.Quaternion.Euler(0, 0, 180);
                attackCollider.position = new UnityEngine.Vector2(transform.position.x - 1f, transform.position.y);
            }
            else if (direction.x > 0)
            {
                attackCollider.rotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                attackCollider.position = new UnityEngine.Vector2(transform.position.x + 1f, transform.position.y);
            }

            if (direction.x < 0)
            {
                transform.localScale = new UnityEngine.Vector3(-1, 1, 1);
            }

            else if (direction.x > 0)
            {
                transform.localScale = new UnityEngine.Vector3(1, 1, 1);
            }

            animator.SetFloat("HorizontalStay", _lastDirection.x);
            animator.SetFloat("VerticalStay", _lastDirection.y);
        }
        else
        {
            return;
        }
    }
   public void TakeDamage(Transform source, int damage)
    {
        if(_canTakeDamage)
        {
            animator.SetBool("TakeDamage", true);
            _canTakeDamage = false;
            curr_hp = Mathf.Max(0, curr_hp - damage);
            Debug.Log($"Current Health - {curr_hp}");

            StartCoroutine(DamageRecoveryRoutine());
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("TakeDamage", false);
        
        yield return new WaitForSeconds(_damageRecoveryTime - 0.1f);
        _canTakeDamage = true;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}