using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpritesSorter : MonoBehaviour
{
    private bool trigger = false;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
        UpdateSortingOrder();
    }

    private void UpdateSortingOrder()
    {
        spriteRenderer.sortingOrder = originalSortingOrder + (trigger ? +1 : 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        trigger = true;
        UpdateSortingOrder();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        trigger = false;
        UpdateSortingOrder();
    }
}
