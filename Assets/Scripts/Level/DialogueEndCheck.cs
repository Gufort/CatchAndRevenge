using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DialogueEndCheck : MonoBehaviour
{
    public DialogueManager dm;
    public void GoToNextScene()
    {
        
        if (dm.isDialogueEnd())
        {
            Debug.Log("Go To Next Scene Called!");
            LevelChange.FadeToLevel();
        }

    }
}
