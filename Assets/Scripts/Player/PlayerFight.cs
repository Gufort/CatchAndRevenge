using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : SoundMaster
{
    [SerializeField] private int damage = 25;
    public Animator animator;
    public PolygonCollider2D attack_pointer;
    private Vector2 direction;

    void Start(){
        ColliderOff();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        if(Input.GetKeyDown(KeyCode.Space)){
            PlaySound(sounds[0], volume: 0.4f, loop: false, p1:0.5f, p2:0.7f);
            Attack();
        }
    }
    void Attack(){
        ColliderOn();
        animator.SetTrigger("Attack");
        Invoke("ColliderOff", 0.5f);
    }

    public void ColliderOff(){
        attack_pointer.enabled = false;
    }
    private void ColliderOn(){
        attack_pointer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.transform.TryGetComponent(out EnemyHP enemy)){
            enemy.TakeDamage(damage);
        }
    }
}
