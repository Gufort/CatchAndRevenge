using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportWithFade : MonoBehaviour
{
    [SerializeField] private GameObject _fadePanel; // Панель затемнения в UI
    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private int _levelNumber;
    [SerializeField] private Vector2 _targetPosition;
    [SerializeField] private VectorValue _playerStorage;

    private void Start()
    {
        if (_fadeAnimator == null && _fadePanel != null)
            _fadeAnimator = _fadePanel.GetComponent<Animator>();
    }

    // Вызывается извне (например, из триггера)
    public void StartTeleport()
    {
        _fadePanel.SetActive(true);
        _fadeAnimator.SetTrigger("Fade");
    }

    // Вызывается в конце анимации (через Animation Event)
    public void OnFadeComplete()
    {
        _playerStorage.initialValue = _targetPosition;
        SceneManager.LoadScene(_levelNumber);
    }
}