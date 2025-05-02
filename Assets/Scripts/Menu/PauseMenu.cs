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
        Debug.Log("Trying to save inventory...");
        SaveInventory();
        PlayerPrefs.SetInt("SceneContinue", scene);
        SceneManager.LoadScene("Menu");
    }

    private void SaveInventory()
    {
        Transform child;
        Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>();
        if (!(inventory == null) && (inventory.slots.Length > 0) && !(inventory.slots[0] == null))
        {
            Debug.Log("Inventory was found!");
            for (int i = 0; i < inventory.slots.Length; i++)
                {
                    Debug.Log("Parent: " + inventory.slots[i].gameObject.name);
                    if (inventory.isFull[i])
                    {
                        if (inventory.slots[i].transform.childCount > 0) {
                            child = inventory.slots[i].transform.GetChild(0);
                            Debug.Log("Child: " + child.gameObject.name);
                            string clearName = child.gameObject.name.Replace("(Clone)", "");
                            PlayerPrefs.SetInt($"Inventory{clearName}", 1);
                        }
                        break;
                    }
                }
        }
        else { Debug.Log("Inventory is missing on this scene"); }
    }
}
