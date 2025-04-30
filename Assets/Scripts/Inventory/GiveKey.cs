using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveKey : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _keyItem;
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private GameObject _levelChange;
    [SerializeField] private GameObject _textUnderLevelChange;
    private int _dialogueID = 9;
    private Inventory _inventory;
    private bool _isTriggered;
 
    void Start()
    {
        _inventory = _player.GetComponent<Inventory>();
    }
 
    void Update()
    {
        if (_dialogueManager != null && _dialogueManager.isTrueEnd && !_isTriggered && (PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}") == 1))
        {
            _isTriggered = true;
            _textUnderLevelChange.SetActive(true);
            _levelChange.SetActive(true);
            for (int i = 0; i < _inventory.slots.Length; i++)
            {
                if (!_inventory.isFull[i] && PlayerPrefs.GetInt($"Inventory{_keyItem.name}", 0) == 0)
                {
                    _inventory.isFull[i] = true;
                    Instantiate(_keyItem, _inventory.slots[i].transform);
                    PlayerPrefs.SetInt($"Inventory{_keyItem.name}", 1);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
