using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void LoadGameScene()
    {
        var myGamesession = FindObjectOfType<GameSession>();
        if (myGamesession != null)
        {
            Destroy(myGamesession);
        }
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
