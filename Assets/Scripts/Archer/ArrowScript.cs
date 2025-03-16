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
    [SerializeField] private float _pushDistance = 0.5f;
    private ArcherAttack _archerAttack;
    private UnityEngine.Vector2 _direction;
    private PlayerController _player;
    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _archerAttack = GetComponent<ArcherAttack>();
        Destroy(gameObject, _timer);
        _player = PlayerController.instance;
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       transform.position += (UnityEngine.Vector3)(_direction * _speed * Time.deltaTime);

        UnityEngine.Vector3 directionToPlayer = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 0, angle));
    }

    public void setDirection(UnityEngine.Vector2 direction){
        _direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform.TryGetComponent(out PlayerController player)){
            UnityEngine.Vector2 pushDirection = _direction.normalized; 
            player.transform.position += (UnityEngine.Vector3)(pushDirection * _pushDistance);
            Destroy(gameObject);
            Debug.Log("Arrow destroyed");
            player.TakeDamage(transform, _damage);
        }
    }
}
