using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToBlackScene : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private int sceneNumber;
    private void Update()
    {
        if (dialogueTrigger.alreadyTriggered && dialogueManager.isTrueEnd)
        {
            Debug.Log("Go To Next Scene Called!");
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
