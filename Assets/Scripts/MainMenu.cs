using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string levelToLoad = "pacman";
    private int targetDifficulty = 0;
    
    public GameObject highscoreWindow;
    public GameObject creditsWindow;

    [SerializeField]
    private string[] level1;
    [SerializeField]
    private string[] level2;

    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject difficultySelectionPanel;
    [SerializeField]
    private GameObject levelSelectionPanel;
    private Panel currentPanel;

    private void Start() {
        GoToMainPanel();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            switch(currentPanel) {
                case Panel.Main:
                    break;
                case Panel.Difficulty:
                    GoToMainMenu();
                    break;
                case Panel.Level:
                    GoToSelectDifficulty();
                    break;
            }
        }
    }

    public void GoToMainPanel() {
        mainPanel.SetActive(true);
        difficultySelectionPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        currentPanel = Panel.Main;
    }

    public void GoToSelectDifficulty() {
        mainPanel.SetActive(false);
        difficultySelectionPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        currentPanel = Panel.Difficulty;
    }

    public void GoToSelectLevel() {
        mainPanel.SetActive(false);
        difficultySelectionPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
        currentPanel = Panel.Level;
    }

    public void SelectDifficulty(int difficulty) {
        targetDifficulty = difficulty;
        GoToSelectLevel();
    }

    public void SelectLevel(int level) {
        if (level == 0) {
            levelToLoad = (string)level1.GetValue(targetDifficulty);
        } else if (level == 1) {
            levelToLoad = (string)level2.GetValue(targetDifficulty);
        }
        StartGame(levelToLoad);
    }

    public void StartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
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

    enum Panel {Main, Level, Difficulty};
}
