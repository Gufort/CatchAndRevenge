using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterDisable : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private int _dialogueID;
    [SerializeField] private GameObject _enemyCounter;
    
    private bool _isTriggered = false;
    

    void Update()
    {
        if (_dialogueManager != null && !_isTriggered && (PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}") == 1))
        {
            _isTriggered = false;
            _enemyCounter.SetActive(false);
        }
    }
}
