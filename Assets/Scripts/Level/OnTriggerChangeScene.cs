using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OnTriggerChangeScene : MonoBehaviour
{
    public int level_number;
    private bool trigger = false;
    public Vector3 position;
    public VectorValue playerStorage;

    void Update(){
        if(trigger) ChangeScene();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")) trigger = true;
    }

    void ChangeScene(){
        playerStorage.initialValue = position;
        SceneManager.LoadScene(level_number);
    }
}
