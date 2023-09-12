using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreVisual : MonoBehaviour
{
    TextMeshProUGUI tmpText;
    int score = 0;
    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        score = GameManager.Instance.currentScore;
        tmpText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if(score != GameManager.Instance.currentScore)
        {
            score = GameManager.Instance.currentScore;
            tmpText.text = "Score: " + score;
        }
    }
}
