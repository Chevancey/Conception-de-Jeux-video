using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;

public class HighScoreBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject lineInstance;
    private TextMeshProUGUI playerText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI dateText;
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
        Instantiate(lineInstance, gameObject.transform);
    }

    private void BlinkScore(int scoreId) {

    }

    private void DisplayHighScores() {

    }
}
