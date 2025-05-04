using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Animator animator;
    public GameObject dialogueBox;
    public Image dialogueImage;
    public bool freezePersons = false;
    public PlayerController player;
    public Animator playerAnimator;
    public PlayerFight playerFight;
    public SoundMaster soundMaster;
    private Queue<string> sentences;
    private Queue<int> order;
    private int sizeDif = 120;
    private int sizeDifImage = 500;
    private bool toRight = true;
    private bool isTrueDialogue = false;
    private int lastPerson = 0;
    private Sprite sprite1;
    private Sprite sprite2;
    public bool isTrueEnd = false;
    public Rigidbody2D rigidbody2d;
    private GameObject inventoryButton;
    private GameObject pauseButton;
    private GameObject playerHP;

    private void Start()
    {
        sentences = new Queue<string>();
        isTrueEnd = PlayerPrefs.GetInt("DialogueEnd", 0) == 0 ? false : true;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //if (dialogue.nameSecond == "")
        //    Debug.Log("Starting convesation! ---> " + dialogue.name);
        //else Debug.Log("Starting convesation! ---> " + dialogue.name + " and " + dialogue.nameSecond);
        isTrueDialogue = dialogue.twoPerson;
        sprite1 = dialogue.sprite;
        sprite2 = dialogue.spriteSecond;
        dialogueImage.sprite = sprite1;
        isTrueEnd = false;
        dialogueBox.SetActive(true);
        animator.SetBool("IsOpen", true);
        if (sentences == null)
        {
            sentences = new Queue<string>();
        }
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            Debug.Log("Loaded new sentence -> " + sentence + " Total: " + sentences.Count());
        }
        if (isTrueDialogue)
        {
            if (dialogue.order.Length > 0){
                lastPerson = dialogue.order[0];
                if (lastPerson == 1) ChangeDialoguePos();
                if (order== null)
                {
                    order = new Queue<int>();
                }
                foreach(int o in dialogue.order)
                {
                    order.Enqueue(o);
                }
            }
        }
        

        if (freezePersons)
        {
            playerAnimator.SetFloat("Horizontal", 0);
            playerAnimator.SetFloat("Vertical", 0);
            playerAnimator.SetFloat("Speed", 0);
            player.canMove = false;
            rigidbody2d.simulated = false;
            soundMaster.StopSound();
            soundMaster.enabled = false;
            playerFight.enabled = false;
        }

        inventoryButton = GameObject.Find("Inventory");
        pauseButton = GameObject.Find("Pause");
        playerHP = GameObject.Find("PlayerHP");
        inventoryButton?.SetActive(false);
        pauseButton?.SetActive(false);
        playerHP?.SetActive(false);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log("Called Display Next Sentence and sentences count is: " + sentences.Count() + " is sentences null? " + (sentences == null));
        if (isDialogueEnd())
        {
            isTrueEnd = true;
            PlayerPrefs.SetInt("DialogueEnd", 1);
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        int cur = 0;
        if (isTrueDialogue)
            cur = order.Dequeue();
            Debug.Log("Get name of cur Person: " + name + " the last Person name was: " + lastPerson);
        Debug.Log(sentence);
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if (isTrueDialogue)
        {
            if (lastPerson != cur)
            {
               Debug.Log("Called Change DialogueBox pos!"); 
               ChangeDialoguePos();
            }
        }
        lastPerson = cur;
    }

    private void ChangeDialoguePos()
    {
        RectTransform rectTransformT = dialogueText.GetComponent<RectTransform>();
        RectTransform rectTransformI = dialogueImage.GetComponent<RectTransform>();
        if (!toRight){
            rectTransformT.anchoredPosition = new Vector3(sizeDif, 0, 0); 
            rectTransformI.anchoredPosition = new Vector3(-sizeDifImage, 0, 0);
            Debug.Log($"DialogueBox pos moved for {sizeDif}!");
            dialogueImage.overrideSprite = sprite1;
        }
        else {
            rectTransformT.anchoredPosition = new Vector3(-sizeDif, 0, 0);
            rectTransformI.anchoredPosition = new Vector3(sizeDifImage, 0, 0);
            Debug.Log($"DialobueBox pos moved for {-sizeDif}!");
            dialogueImage.overrideSprite = sprite2;
        }
        toRight = !toRight;
    }

    public bool isDialogueEnd()
    {
        return (sentences.Count() == 0);
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of Conversation!");
        animator.SetBool("IsOpen", false);
        dialogueBox.SetActive(false);
        if (freezePersons)
        {
            player.canMove = true;
            rigidbody2d.simulated = true;
            soundMaster.enabled = true;
            playerFight.enabled = true;
        }
        inventoryButton?.SetActive(true);
        pauseButton?.SetActive(true);
        playerHP?.SetActive(true);
    }
}
