using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public GameObject panel;
    private bool trigger;

    private void OnTriggerEnter2D(Collider2D other){
         if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            trigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
             panel.SetActive(false);
            trigger = false;
        }
    }


    void Start(){
        panel.SetActive(false);
    }

   private void Update()
    {
        if (trigger && Input.GetKeyDown(KeyCode.E))
        {
            LevelChange.FadeToLevel();
        }
    }
}
