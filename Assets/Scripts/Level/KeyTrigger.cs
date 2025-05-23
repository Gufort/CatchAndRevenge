using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTrigger : MonoBehaviour
{
    public GameObject panel;
    public GameObject imageComponent; 
    public GameObject canvas;
    public GameObject textBox;
    [SerializeField] ImageAnimator _imageAnimator;
    private bool trigger;

    private void OnTriggerEnter2D(Collider2D other){
        if ((MovePuzzle.end) && (other.CompareTag("Player")))
        {
            panel.SetActive(true);
            trigger = true;
            canvas.SetActive(true);
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

   private void Update(){
        if (trigger && Input.GetKeyDown(KeyCode.E))
        {
            textBox.SetActive(false);
            imageComponent.SetActive(true);
        }
        if (_imageAnimator.animationFinished) {
            LevelChange.FadeToLevel();
        }
    }
}
