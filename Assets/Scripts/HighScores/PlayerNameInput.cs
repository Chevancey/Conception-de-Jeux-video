using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerNameInput : MonoBehaviour
{
    private TextMeshProUGUI scorePrompt;
    private TMP_InputField inputField;
    // Start is called before the first frame update
    private void Start()
    {
        scorePrompt = GetComponentInChildren<TextMeshProUGUI>();
        inputField = GetComponentInChildren<TMP_InputField>();
        EventSystem.current.SetSelectedGameObject(inputField.gameObject,null);
    }

    public void SetScore(int score) {
        scorePrompt.text = "Your score : " + score.ToString("000000000");
    }


    public void OnInputValidation() {
        string scoreString = scorePrompt.text.Replace("Your score : ","");
        int score = int.Parse(scoreString);
        string name = inputField.text;
        Debug.Log("name: " + name);
        HighscoreManager.Instance.WriteHighScore(score, name, DateTime.Now);
        gameObject.SetActive(false);
    }
}
