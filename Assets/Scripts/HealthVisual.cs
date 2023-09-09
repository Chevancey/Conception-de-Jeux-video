using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthVisual : MonoBehaviour
{
    TextMeshProUGUI tmpText;
    int health = 0;
    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = GameManager.Instance.currentLives;
        tmpText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health != GameManager.Instance.currentLives)
        {
            health = GameManager.Instance.currentLives;
            tmpText.text = "Health: " + health;
        }
    }
}
