using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SetCameraBounds : MonoBehaviour
{
    public CinemachineConfiner confiner;
    public PolygonCollider2D newBounds;
    private bool trigger = false;

    void Start()
    {
        if (confiner != null && newBounds != null)
        {
            confiner.m_BoundingShape2D = newBounds;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !trigger)
        {
            trigger = true;
            if (confiner != null && newBounds != null)
            {
                confiner.m_BoundingShape2D = newBounds;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) trigger = false;
    }
}
