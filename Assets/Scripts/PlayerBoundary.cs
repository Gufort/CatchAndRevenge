using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class PlayerBoundary : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        polygonCollider = FindObjectOfType<PolygonCollider2D>();
    }

    void Update()
    {
        Vector2 position = transform.position;
        if (!polygonCollider.OverlapPoint(position))
        {
            Vector2 closestPoint = polygonCollider.ClosestPoint(position);
            transform.position = closestPoint;
        }
    }
}
