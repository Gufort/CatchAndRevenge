using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Vector2 teleport;
    public GameObject player;
    private bool trigger = false;
    public CinemachineConfiner confiner;
    public PolygonCollider2D newBounds;

    void Start()
    {
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = newBounds;
        }
    }

    public void Teleport()
    {
        player.transform.position = teleport;
        Debug.Log("Игрок телепортирован");

        if (confiner != null && newBounds != null)
        {
            confiner.m_BoundingShape2D = newBounds;
            Debug.Log("Границы камеры обновлены");
        }
    }

    void Update()
    {
        if (trigger)
        {
            Teleport();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
            Debug.Log("Игрок вошел в триггер");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = false;
            Debug.Log("Игрок вышел из триггера");
        }
    }
}


