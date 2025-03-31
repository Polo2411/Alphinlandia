using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    // Static to have access through all documents
    public bool isPaused;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("ActualDialog", 0); // Reset Dialog
        SceneManager.LoadScene("main_menu");
    }
}
