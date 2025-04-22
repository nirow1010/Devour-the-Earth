using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene("Hub");
    }

    public void Regroup() 
    {
        SceneManager.LoadScene("Hub");
    }

    public void GoToMainMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GoToControls() 
    {
        SceneManager.LoadScene("Controls");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
