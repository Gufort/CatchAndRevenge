using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterractWithChest : MonoBehaviour
{
    public TMP_Text InteractInviteText;
    public GameObject PuzzleActivate;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractInviteText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractInviteText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && InteractInviteText.gameObject.activeSelf == true)
        {
            PuzzleActivate.SetActive(true);
        }
    }
}
