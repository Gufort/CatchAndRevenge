using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private VectorValue pos;
    public void QuitGame()
    {   
        Debug.Log("Игра закрыта");
        Application.Quit();
    }
    public void PlayNewGame()
    {
        PlayerPrefs.DeleteAll();
        pos.initialValue = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
