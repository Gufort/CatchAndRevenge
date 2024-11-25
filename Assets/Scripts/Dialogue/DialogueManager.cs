using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;
    public Animator animator;
    private Queue<string> sentences;
    public GameObject dialogueBox;
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting convesation! ---> " + dialogue.name);
        dialogueBox.SetActive(true);
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
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
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log("Called Display Next Sentence and sentences count is: " + sentences.Count() + " is sentences null? " + (sentences == null));
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of Conversation!");
        animator.SetBool("IsOpen", false);
        dialogueBox.SetActive(false);
    }
}
