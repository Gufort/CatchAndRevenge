using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [Header("Настройки стрелы: \n")]
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _timer = 5f;
    [SerializeField] private int _damage = 20;
    private ArcherAttack _archerAttack;
    private UnityEngine.Vector2 _direction;
    private PlayerController _player;

    private void Start()
    {
        _archerAttack = GetComponent<ArcherAttack>();
        Destroy(gameObject, _timer);
        _player = PlayerController.instance;
    }

    private void Update()
    {
       transform.position += (UnityEngine.Vector3)(_direction * _speed * Time.deltaTime);
    }

    public void setDirection(UnityEngine.Vector2 direction){
        _direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform.TryGetComponent(out PlayerController player)){
            Destroy(gameObject);
            Debug.Log("Arrow destroyed");
            player.TakeDamage(transform, _damage);
        }
    }
}
