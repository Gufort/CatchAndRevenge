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
    public GameObject giveList;
    public Rigidbody2D rigidbody2d;
    private GameObject inventoryButton;
    private GameObject pauseButton;
    private GameObject playerHP;
    private int _dialogueID = 11;

    public static string answer = "ответственность";

    void Start()
    {
        activatedYet = (PlayerPrefs.GetInt("MazeActivated", 0) == 0) ? false : true;
        inventoryButton = GameObject.Find("Inventory");
        pauseButton = GameObject.Find("Pause");
        playerHP = GameObject.Find("PlayerHP");
    }

    void Update()
    {
        //Debug.Log("Dialogue 10 is triggered: " + PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}", 0));
        if (dialogueManager != null && dialogueManager.isTrueEnd && !activatedYet && (PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}", 0) == 1))
        {
            inventoryButton?.SetActive(false);
            pauseButton?.SetActive(false);
            playerHP?.SetActive(false);
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
                PlayerPrefs.SetInt("MazeActivated", 1);
                mysteryInputWindow.SetActive(false);
                inventoryButton?.SetActive(true);
                pauseButton?.SetActive(true);
                playerHP?.SetActive(true);
                rigidbody2d.simulated = true;
                playerController.canMove = true;
                dialogueTrigger.TriggerDialogue();
                giveList.SetActive(true);
            }
        }
    }
}
