using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuest : MonoBehaviour
{
    public DialogueManager dm;
    public GameObject dialogueBox;
    public GameObject MushroomsBasket;
    private bool IsTriggered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTriggered)
        {
            if (dm.isDialogueEnd()&&(dialogueBox.activeInHierarchy))
            {
                IsTriggered = true;
                MushroomsBasket.SetActive(true);
            }
        }
    }
}
