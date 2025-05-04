using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationAfterDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    public GameObject dialogueTrigger;
    private bool isTriggered;
    [SerializeField ]private int _dialogueID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager != null && dialogueManager.isTrueEnd && !isTriggered && (PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}") == 1)
)
        {
            isTriggered = true;
            dialogueTrigger.SetActive(true);
        }
    }
}
