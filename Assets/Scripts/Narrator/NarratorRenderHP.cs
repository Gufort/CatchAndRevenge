using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarratorRenderHP : MonoBehaviour
{
    [SerializeField] private NarratorHP _narratorSO;
    public TMP_Text healthText;
    void Update()
    {
        if (healthText != null) healthText.text = _narratorSO._currentHP.ToString();
    }
}
