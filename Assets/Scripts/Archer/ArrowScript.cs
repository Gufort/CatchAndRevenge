using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [Header("Настройки стрелы: \n")]
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _timer = 5f;
    private UnityEngine.Vector2 _direction;
    private PlayerController _player;

    private void Start()
    {
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            Destroy(gameObject);
            _player.TakeDamage(gameObject.transform, 20);
        }
    }
}
