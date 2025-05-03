using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBollScript : MonoBehaviour
{
    [Header("Настройки фаербола: \n")]
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _timer = 5f;
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _pushDistance = 0.5f;
    private NarratorAttack _narratorAttack;
    private UnityEngine.Vector2 _direction;
    private PlayerController _player;
    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _narratorAttack = GetComponent<NarratorAttack>();
        Destroy(gameObject, _timer);
        _player = PlayerController.instance;
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = UnityEngine.Quaternion.Euler(0, 0, angle);
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
            UnityEngine.Vector2 pushDirection = _direction.normalized; 
            player.transform.position += (UnityEngine.Vector3)(pushDirection * _pushDistance);
            Destroy(gameObject);
            Debug.Log("Fireboll destroyed");
            player.TakeDamage(transform, _damage);
        }
    }
}
