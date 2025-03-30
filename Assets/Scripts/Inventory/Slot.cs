using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;
    public GameObject Player;
    public GameObject Panel;

    private void Start()
    {
        //inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventory = Player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }

    public void Open()
    {
        foreach (Transform child in transform)
        {
            var item = child.GetComponent<Spawn>();
            if (item.CompareTag("List"))
            {
                Panel.SetActive(true);
            }
        }
    }
}
