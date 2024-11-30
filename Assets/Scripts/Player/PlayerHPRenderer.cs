using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHPRenderer : MonoBehaviour
{
    public TMP_Text healthText;
    void Update()
    {
        if (healthText != null) healthText.text = "HP: " + PlayerController.curr_hp_to_renderer.ToString();
    }
}
