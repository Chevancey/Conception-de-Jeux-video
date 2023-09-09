using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button playAgain;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip victoryClip;
    // Start is called before the first frame update
    private void Awake()
    {
        playAgain.onClick.AddListener(() =>
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("Pacman");
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        });
    }

    private void OnEnable()
    {
        music.clip = victoryClip;
        music.Play();
    }
}
