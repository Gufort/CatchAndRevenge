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
        

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
