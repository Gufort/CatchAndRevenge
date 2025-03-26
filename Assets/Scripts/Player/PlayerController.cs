using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SoundMaster
{
    [SerializeField] private int max_hp;
    [SerializeField] private float _damageRecoveryTime = 0.5f;
    [SerializeField] private bool _canTakeDamage;
    static public int curr_hp_to_renderer;

    public static PlayerController instance{get;private set;}
    public float speed;
    public Animator animator;
    private Vector2 direction;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private int curr_hp;
    public VectorValue pos;
    public Transform attackCollider;
    public bool canMove = true;

    private void Awake() {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
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
            if (curr_hp == 0) curr_hp = 100;
            curr_hp_to_renderer = curr_hp;
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);


            if (direction.sqrMagnitude > 0)
            {
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
                attackCollider.rotation = Quaternion.Euler(0, 0, 90);
                attackCollider.position = new Vector2(transform.position.x, transform.position.y + 1f);
            }
            else if (direction.y < 0)
            {
                attackCollider.rotation = Quaternion.Euler(0, 0, -90);
                attackCollider.position = new Vector2(transform.position.x, transform.position.y - 1f);
            }
            else if (direction.x < 0)
            {
                attackCollider.rotation = Quaternion.Euler(0, 0, 180);
                attackCollider.position = new Vector2(transform.position.x - 1f, transform.position.y);
            }
            else if (direction.x > 0)
            {
                attackCollider.rotation = Quaternion.Euler(0, 0, 0);
                attackCollider.position = new Vector2(transform.position.x + 1f, transform.position.y);
            }

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            return;
        }
    }
    public void TakeDamage(Transform source, int damage){
        if(_canTakeDamage){
            _canTakeDamage = false;
            curr_hp = Mathf.Max(0,curr_hp-=damage);
            Debug.Log($"Current Health - {curr_hp}");

            StartCoroutine(DamageRecoveryRoutine());
        }
    }

    private IEnumerator DamageRecoveryRoutine(){
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}