using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterractWithChest : MonoBehaviour
{
    public GameObject InteractInviteBox;
    public TMP_Text InteractInviteText;
    public GameObject PuzzleActivate;

    private GameObject pauseButton;
    private GameObject playerHP;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractInviteBox.SetActive(true);
            InteractInviteText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractInviteText.gameObject.SetActive(false);
            InteractInviteBox.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && InteractInviteText.gameObject.activeSelf == true)
        {
            MovePuzzle.end = false;
            PuzzleComplete.curElement = 0;
            PuzzleActivate.SetActive(true);

            pauseButton = GameObject.Find("Pause");
            playerHP = GameObject.Find("PlayerHP");
            pauseButton?.SetActive(false);
            playerHP?.SetActive(false);
        }
    }
}
