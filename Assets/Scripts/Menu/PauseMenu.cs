using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("!Exiting to menu, saving information!");
        int scene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current scene: " + scene);
        PlayerPrefs.SetInt("SceneContinue", scene);
        Debug.Log("Player X pos: " + player.transform.position.x);
        // PlayerPrefs.SetFloat("PosX", player.transform.position.x);
        // PlayerPrefs.SetFloat("PosY", player.transform.position.y);
        // PlayerPrefs.SetFloat("PosZ", player.transform.position.z);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
    }
}
