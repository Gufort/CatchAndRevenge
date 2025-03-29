using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveList : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject listItem;
    private Inventory inventory;
    private bool isTriggered;

    void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    void Update()
    {
        if (dialogueManager != null && dialogueManager.isTrueEnd && !isTriggered)
        {
            isTriggered = true;
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (!inventory.isFull[i] && PlayerPrefs.GetInt($"Inventory{listItem.name}", 0) == 0)
                {
                    inventory.isFull[i] = true;
                    Instantiate(listItem, inventory.slots[i].transform);
                    PlayerPrefs.SetInt($"Inventory{listItem.name}", 1);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
