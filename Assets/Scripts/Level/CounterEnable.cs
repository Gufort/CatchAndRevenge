using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CounterEnable : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private int _dialogueID;
    [SerializeField] private GameObject _enemyCounter;
    
    private bool _isTriggered = false;
    

    void Update()
    {
        if (_dialogueManager != null && _dialogueManager.isTrueEnd && !_isTriggered && (PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}") == 1))
        {
            _isTriggered = false;
            _enemyCounter.SetActive(true);
        }
    }
}
