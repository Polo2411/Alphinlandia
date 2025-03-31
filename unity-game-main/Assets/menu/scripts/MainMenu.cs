using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("HelperActualDialog", 0);
        PlayerPrefs.SetInt("DialogActualDialog", 0);
        PlayerPrefs.SetInt("BookCurrentMemoryIndex", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("OliviaDoor", 0);
        PlayerPrefs.SetInt("ConstanceDoor", 0);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Real_world");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
