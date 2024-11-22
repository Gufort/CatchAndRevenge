using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogueTrigger dt;
    // Start is called before the first frame update
    void Start()
    {
        if (dt != null)
            dt.TriggerDialogue();
    }
}
