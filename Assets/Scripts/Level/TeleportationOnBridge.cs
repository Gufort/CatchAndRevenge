using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TeleportationOnBridge : MonoBehaviour
{
    public Vector2 teleport;
    public GameObject player;
    private bool trigger = false;
    public GameObject dialogueTrigger;

    public AudioClip soundClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Teleport()
    {
        dialogueTrigger.SetActive(true);

        player.transform.position = teleport;
        if (audioSource != null && soundClip != null)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
        Debug.Log("Игрок телепортирован");
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


