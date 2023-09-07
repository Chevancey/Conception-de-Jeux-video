using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad;
    public GameObject highscoreWindow;
    public GameObject creditsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ShowHighscore()
    {
        //Show highscore
        highscoreWindow.SetActive(true);

    }

    public void Credits()
    {
        creditsWindow.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsWindow.SetActive(false);
    }

    public void CloseHighscoreWindow()
    {
        highscoreWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
