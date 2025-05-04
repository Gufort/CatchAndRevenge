using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActivateTeleportCollider : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private int _dialogueID;
    [SerializeField] private GameObject _collider;
    private bool _isTriggered;

    private void Update()
    {
        if (_dialogueManager != null && _dialogueManager.isTrueEnd && !_isTriggered && (PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}") == 1)){
            _isTriggered = true;
            _collider.SetActive(true);
        }
    }

}
