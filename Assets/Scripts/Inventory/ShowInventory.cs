using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowInventory : MonoBehaviour
{
    public GameObject inventory;
    public Animator animator;

    public void OpenInventory()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
            animator.SetBool("InventoryOpen", true);
        }
        else
        {
            animator.SetBool("InventoryOpen", false);
            StartCoroutine(delay());
            inventory.SetActive(false);
        }
    }

    private IEnumerator delay()
    {
        yield return null;
        yield return null;
        yield return null;
    }
}
