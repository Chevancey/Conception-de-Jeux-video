using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvaPause : MonoBehaviour
{

    [SerializeField] Canvas canvasPauseMenu;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;

    [SerializeField] private AudioSource music;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            ResumeGame();
            Time.timeScale = 1f;
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        });
    }
    private void Start()
    {
        canvasPauseMenu.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1f)
            {
                PauseGame();
                Time.timeScale = 0f;
            }
            else
            {
                ResumeGame();
                Time.timeScale = 1f;
            }

        }
    }

    private void ResumeGame()
    {
        music.Play();
        canvasPauseMenu.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void PauseGame()
    {
        music.Pause();
        canvasPauseMenu.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
