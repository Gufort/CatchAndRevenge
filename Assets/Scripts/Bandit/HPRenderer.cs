using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HPRenderer : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text healthText;
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + EnemyHP.curr_hp_to_renderer.ToString();
            if(EnemyHP.curr_hp_to_renderer==0) EnemyHP.curr_hp_to_renderer = 100;
        }
    }
}
