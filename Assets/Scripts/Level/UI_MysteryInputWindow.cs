using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_MysteryInputWindow : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject mysteryInputWindow;
    public TMP_InputField mysteryInputField;
    private bool activatedYet = false;
    public Animator animator;
    public PlayerController playerController;
    public DialogueTrigger dialogueTrigger;
    public Rigidbody2D rigidbody2d;

    public static string answer = "ответственность";
    void Update()
    {
        if (dialogueManager != null && dialogueManager.isTrueEnd && !activatedYet)
        {
            mysteryInputWindow.SetActive(true);
            rigidbody2d.simulated = false;
            activatedYet = true;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
            playerController.canMove = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(mysteryInputField.text))
        {
            if (mysteryInputField.text.ToLower() == answer)
            {
                mysteryInputWindow.SetActive(false);
                rigidbody2d.simulated = true;
                playerController.canMove = true;
                dialogueTrigger.TriggerDialogue();
            }
        }
    }
}
