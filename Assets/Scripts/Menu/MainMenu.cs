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
        float volume = PlayerPrefs.GetFloat("BackgroundVolume", 1f);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("BackgroundVolume", volume);
        pos.initialValue = Vector3.zero;
        MovePuzzle.end = false;
        PuzzleComplete.curElement = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("SceneContinue"))
        {
            int sceneInd = PlayerPrefs.GetInt("SceneContinue");
            pos.initialValue = Vector3.zero;
            SceneManager.LoadScene(sceneInd);
        }
    }

}
