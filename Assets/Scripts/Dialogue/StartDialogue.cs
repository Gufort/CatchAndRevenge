using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public int DialogueID;
    public bool isAutostart = false;
    public DialogueTrigger dt;
    private string DialogueTriggered;

    private void Awake()
    {
        DialogueTriggered = $"DialogueTriggered+{DialogueID}";
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt(DialogueTriggered, 0) == 1)
        {
            this.enabled = false;
        }
        else if (isAutostart && PlayerPrefs.GetInt(DialogueTriggered, 0) == 0)
        {
            dt.TriggerDialogue();
            PlayerPrefs.SetInt(DialogueTriggered, 1);
            PlayerPrefs.Save(); 
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt(DialogueTriggered, 0) == 0)
        {
            dt.TriggerDialogue();
            PlayerPrefs.SetInt(DialogueTriggered, 1);
            PlayerPrefs.Save(); 
        }
    }
}
