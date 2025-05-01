using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorterForBandit : MonoBehaviour
{   
    [SerializeField] private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private bool trigger = false;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
        UpdateSortingOrder();
    }

    private void UpdateSortingOrder()
    {
        spriteRenderer.sortingOrder = originalSortingOrder + (trigger ? + 1 : 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out PlayerController player) 
        && other.IsTouching(_boxCollider2D)){
            trigger = true;
            UpdateSortingOrder();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out PlayerController player) 
        && !other.IsTouching(_boxCollider2D)
           ){
            trigger = false;
            UpdateSortingOrder();
        }
    }
}
