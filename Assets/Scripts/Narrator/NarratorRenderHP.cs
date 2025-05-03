using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarratorRenderHP : MonoBehaviour
{
    public TMP_Text healthText;
    void Update()
    {
        if (healthText != null) healthText.text = NarratorHP.curr_hp_to_renderer.ToString();
    }
}
