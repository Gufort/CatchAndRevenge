using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool alreadyTriggered = false;
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        if (!alreadyTriggered)
        {
            alreadyTriggered = true;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
