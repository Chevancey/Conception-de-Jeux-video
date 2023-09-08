using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HighScoreBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject lineInstance;
    private TextMeshProUGUI playerText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI dateText;

    private List<GameObject> highscoreObjects = new();
    // Start is called before the first frame update
    void Start()
    {
        if (lineInstance != null) {
            foreach (TextMeshProUGUI textContainer in lineInstance.GetComponentsInChildren<TextMeshProUGUI>()) {
                switch (textContainer.tag) {
                    case "Player":
                        playerText = textContainer;
                        break;
                    case "Score":
                        scoreText = textContainer;
                        break;
                    case "Date":
                        dateText = textContainer;
                        break;
                }
            }
        }
        UpdateHighScores();
    }

    public void ShowHighScore() {
        gameObject.SetActive(true);
        UpdateHighScores();
    }

    public void HideHighScores() {
        gameObject.SetActive(false);
    }

    private void AddLine(string playerName, int score, DateTime date) {
        if (playerText != null) {
            playerText.text = playerName;
        }
        if (scoreText != null) {
            scoreText.text = score.ToString("000000000");
        }
        if (dateText != null) {
            dateText.text = date.ToString("dd/MM");
        }
        highscoreObjects.Append(Instantiate(lineInstance, gameObject.transform));
    }

    private void AddLine(string playerName, int score, string date) {
        if (playerText != null) {
            playerText.text = playerName;
        }
        if (scoreText != null) {
            scoreText.text = score.ToString("000000000");
        }
        if (dateText != null) {
            dateText.text = date;
        }
        highscoreObjects.Add(Instantiate(lineInstance, gameObject.transform));
    }

    private void CleanLines() {
        if (highscoreObjects.Count > 0) {
            foreach(GameObject highscoreObject in highscoreObjects) {
                Destroy(highscoreObject);
            }
            highscoreObjects.Clear();
        }
    }

    private void BlinkScore(int scoreId) {

    }

    private void UpdateHighScores() {
        HighscoreManager.Instance.UpdateHighScores();
        HighscoreManager.HighScoreItem[] highscoresItems = HighscoreManager.Instance.ReadHighScores();
        List<HighscoreManager.HighScoreItem> highscoreList = new(highscoresItems);
        List<HighscoreManager.HighScoreItem> top8Scores;
        if (highscoreList.Count > 8) {
            top8Scores = highscoreList.GetRange(0,8);
        } else {
            top8Scores = highscoreList;
        }

        CleanLines();
        foreach (HighscoreManager.HighScoreItem scoreItem in top8Scores) {
            AddLine(scoreItem.name, scoreItem.score, scoreItem.date);
        }
    }

}
