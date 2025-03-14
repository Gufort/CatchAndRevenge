using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    [Header("Настройки атаки: \n")]
    [SerializeField] private float _attackRange = 15f;
    [SerializeField] private float _attackCoolDown = 5f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private GameObject _prefabArrow;
    private float _lastShotTime = 0f;
    private ArcherMove _archerMove;

    private void Awake()
    {
        _archerMove = GetComponent<ArcherMove>();
    }

    private void Attack(){
        _lastShotTime = Time.time;
        GameObject current_arrow = Instantiate(_prefabArrow,_attackPoint.position, Quaternion.identity);
        ArrowScript arrow = current_arrow.GetComponent<ArrowScript>();

        Vector2 direction = (PlayerController.instance.transform.position - _attackPoint.position).normalized;
        arrow.setDirection(direction);

    }

    public void tryAttack(){
        float distanceToPlayer = UnityEngine.Vector2.Distance(transform.position, PlayerController.instance.transform.position);
        if(distanceToPlayer <= _attackRange && Time.time >= _lastShotTime + _attackCoolDown)
            Attack();
    }

    public float getAttackRange() { return _attackRange; }

    
}
