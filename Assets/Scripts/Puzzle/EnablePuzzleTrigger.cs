using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnablePuzzleTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    private bool IsTriggered;
    public DialogueTrigger dialogueTrigger;

    public void EnableDialgoue()
    {
        if (dialogueManager != null && dialogueManager.isTrueEnd && MovePuzzle.end && !IsTriggered)
        {
            IsTriggered = true;
            dialogueTrigger.TriggerDialogue();
            GameObject.Destroy(gameObject);
        }
        
    }
}
