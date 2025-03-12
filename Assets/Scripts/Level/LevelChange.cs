using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelChange : MonoBehaviour
{
    public static Animator anim;
    public int level_number;
    public Vector3 position;
    public VectorValue playerStorage;

    private void Start(){
        anim = GetComponent<Animator>();
    }

    public static void FadeToLevel(){
        anim.SetTrigger("Fade");
    }

    public void OnFadeComplete(){
        playerStorage.initialValue = position;
        SceneManager.LoadScene(level_number);
    }
}
