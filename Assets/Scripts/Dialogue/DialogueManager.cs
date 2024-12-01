using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public EnemyScript[] enemies;
    private Queue<string> sentences;
    private int sizeDif = 120;
    private int sizeDifImage = 500;
    private bool isFirst = true;
    private bool isTrueDialogue = false;
    private Sprite sprite1;
    private Sprite sprite2;
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue.nameSecond == "")
            Debug.Log("Starting convesation! ---> " + dialogue.name);
        else Debug.Log("Starting convesation! ---> " + dialogue.name + " and " + dialogue.nameSecond);
        isTrueDialogue = dialogue.twoPerson;
        sprite1 = dialogue.sprite;
        sprite2 = dialogue.spriteSecond;
        dialogueImage.sprite = sprite1;
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
        if (freezePersons)
        {
            playerAnimator.enabled = false;
            player.enabled = false;
            if (isTrueDialogue)
            {
                foreach(EnemyScript enemy in enemies)
                    enemy.enabled = false;
            }
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log("Called Display Next Sentence and sentences count is: " + sentences.Count() + " is sentences null? " + (sentences == null));
        if (isDialogueEnd())
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if (isTrueDialogue)
        {
            Debug.Log("Called Change DialogueBox pos!");
            ChangeDialoguePos();
        }
    }

    private void ChangeDialoguePos()
    {
        RectTransform rectTransformT = dialogueText.GetComponent<RectTransform>();
        RectTransform rectTransformI = dialogueImage.GetComponent<RectTransform>();
        if (isFirst){
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
        isFirst = !isFirst;
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
            playerAnimator.enabled = true;
            player.enabled = true;
            if (isTrueDialogue)
            {
                foreach(EnemyScript enemy in enemies)
                    enemy.enabled = true;
            }
        }
    }
}
