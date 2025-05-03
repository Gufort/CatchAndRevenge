using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFadeToTransition : MonoBehaviour
{
    [SerializeField] private TeleportWithFade _teleportController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _teleportController.StartTeleport(); 
        }
    }
}
