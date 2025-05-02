using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_BridgeMysteryInputWindow : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject mysteryInputWindow;
    public TMP_InputField mysteryInputField;
    private bool activatedYet = false;
    public Animator animator;
    public PlayerController playerController;
    public DialogueTrigger dialogueTrigger;
    public GameObject giveList;
    public Rigidbody2D rigidbody2d;
    private bool playerInTrigger = false;
    public bool solved = false;
    public GameObject dialogueAfterMystery;

    public static string answer = "совесть";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    void Update()
    {
        if (dialogueManager != null && dialogueManager.isTrueEnd && !activatedYet && playerInTrigger)
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
                giveList.SetActive(true);
                solved = true;
            }
        }
        if (solved)
        {
            dialogueAfterMystery.SetActive(true);
        }
    }
}
