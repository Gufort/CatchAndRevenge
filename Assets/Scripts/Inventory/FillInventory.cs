using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillInventory : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject[] items;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = player.GetComponent<Inventory>();
        foreach (var item in items)
        {
            if (PlayerPrefs.GetInt($"Inventory{item.name}", 0) == 1)
            {
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (!inventory.isFull[i])
                    {
                        inventory.isFull[i] = true;
                        Instantiate(item, inventory.slots[i].transform);
                        PlayerPrefs.SetInt($"Inventory{item.name}", 1);
                        break;
                    }
                }
            }
        }
    }
}
