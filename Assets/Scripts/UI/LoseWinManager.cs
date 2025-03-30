using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseWinManager : MonoBehaviour
{
    [SerializeField] private String levelName;
    [SerializeField] private String currentLevelName;

    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(levelName);
    }
    public void ReloadLevel() 
    {
        SceneManager.LoadScene(currentLevelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
