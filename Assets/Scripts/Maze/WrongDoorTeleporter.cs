using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongDoorTeleporter : MonoBehaviour
{
    private Vector2 teleport = new Vector2(0.0f, 0.0f);
    public GameObject player;
    private bool trigger = false;
    public CinemachineConfiner confiner;
    public PolygonCollider2D newBounds;
    static public bool dialogTrigger = false;

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
        dialogTrigger = true;
        Debug.Log("Игрок телепортирован в начало");

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


