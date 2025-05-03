using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NarratorDeathTeleport : MonoBehaviour
{
    [SerializeField] private NarratorHP narrator;
    [SerializeField] private GameObject teleportCollider;
    private bool _isTriggered = false;
    void Update()
    {
        if (!_isTriggered && narrator._isDie)
        {
            _isTriggered = true;
            teleportCollider.SetActive(true);
        }
    }
}
