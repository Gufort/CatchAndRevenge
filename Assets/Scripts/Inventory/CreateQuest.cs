using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuest : MonoBehaviour
{
    public DialogueManager dm;
    public GameObject dialogueBox;
    public GameObject MushroomsBasket;
    private bool IsTriggered;
    [SerializeField] private int _dialogueID = 7;

    void Update()
    {
        if (!IsTriggered)
        {
            if (dm.isTrueEnd && (PlayerPrefs.GetInt($"DialogueTriggered+{_dialogueID}") == 1))
            {
                IsTriggered = true;
                MushroomsBasket.SetActive(true);
            }
        }
    }
}
