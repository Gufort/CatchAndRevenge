using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SoundMaster
{
    public float speed;
    public Animator animator;
    private Vector2 direction;
    private Rigidbody2D rb;
    private bool isMoving = false;
    public VectorValue pos;
     public Transform attackCollider;

    void Start()
    {
        transform.position = pos.initialValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

        if (direction.sqrMagnitude > 0)
        {
            if (!isMoving)
            {
                PlaySound(sounds[0], volume: 0.4f, loop: true, p1:0.9f, p2:1f);
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
             transform.localScale = new Vector3(-1, 1, 1);
            attackCollider.rotation = Quaternion.Euler(0, 0, 180);
            attackCollider.position = new Vector2(transform.position.x - 1f, transform.position.y);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            attackCollider.rotation = Quaternion.Euler(0, 0, 0);
            attackCollider.position = new Vector2(transform.position.x + 1f, transform.position.y);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
