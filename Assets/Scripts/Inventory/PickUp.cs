using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public GameObject Player;

    private void Start()
    {
        //inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventory = Player.GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.TryGetComponent(out PlayerController player))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (!inventory.isFull[i] && PlayerPrefs.GetInt($"Inventory{slotButton.name}", 0) == 0)
                {
                    inventory.isFull[i] = true;
                    Instantiate(slotButton, inventory.slots[i].transform);
                    PlayerPrefs.SetInt($"Inventory{slotButton.name}", 1);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
