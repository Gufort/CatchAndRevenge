using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSentence : MonoBehaviour
{
    [SerializeField] private DialogueManager dm;
    [SerializeField] bool sceneTransition;
    [SerializeField] private DialogueEndCheck dialogueEndCheck;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            dm.DisplayNextSentence();
            if (sceneTransition)
                dialogueEndCheck.GoToNextScene();
        }
    }
}
