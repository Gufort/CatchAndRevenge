using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Vector2 _newPosition;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _fade;
    [SerializeField] private float _delay;
    private PlayerController _playerController;

    private void Update()
    {
        _playerController = GetComponent<PlayerController>();
        if(_playerController.curr_hp <= 0) playerDie();
    }

    private IEnumerator DelayForFade(float delay){
        yield return new WaitForSeconds(delay);
        _playerController.transform.position = _newPosition;
        _playerController.curr_hp = 100;
        _fade.SetActive(false);
    }

    public void playerDie(){
        _fade.SetActive(true);
        StartCoroutine(DelayForFade(_delay));
    }

}
