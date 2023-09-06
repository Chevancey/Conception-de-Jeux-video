using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public GhostController[] _ghost;
    public GhostController[] ghost
    {
        get => _ghost;
    }

    public PlayerController pacman;

    public Transform pellets;

    public int currentScore { get; private set; }
    public int currentLives { get; private set; }
    public int pointMultiplier { get; private set; } = 2;


    [SerializeField] AudioClip muchSound1;
    [SerializeField] AudioClip muchSound2;
    bool munchingAlternate = false;

    void Start()
    {
        StatNewGame();
    }

    void Update()
    {
        if (currentLives <= 0 && Input.anyKeyDown) 
        {
            StatNewGame();
        }
    }

    private void StatNewGame() 
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void GameOver() 
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            _ghost[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void NewRound() 
    {
        foreach (Transform pellet in pellets) 
        { 
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState() 
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            _ghost[i].gameObject.SetActive(true);
        }

        pacman.gameObject.SetActive(true);
    }

    private void SetScore(int score) 
    {
        currentScore = score;
    }

    private void SetLives(int lives) 
    {
        currentLives = lives;
    }

    public void GhostDeath(GhostController ghost) 
    {
        SetScore(currentScore + ghost.points);
    }

    private void ResetMultiplier() 
    {
        pointMultiplier = 1;
    }

    public void PelletEaten(Pellet pellet)
    {
        SetScore(currentScore + (pellet.points * pointMultiplier));

        pellet.gameObject.SetActive(false);

        if (munchingAlternate)
            SoundManager.Instance?.playClipAtPosition(transform.position, muchSound1);
        else
            SoundManager.Instance?.playClipAtPosition(transform.position, muchSound2);

        munchingAlternate = !munchingAlternate;

        if (HasEatenAll()) 
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(ResetState), 3.0f);
        }
    }

    public void PowPelletEaten(PowPellet powPellet)
    {
        PelletEaten(powPellet);
        CancelInvoke();
        Invoke(nameof(ResetMultiplier), powPellet.duration);
        
        // add ghost being scared off
    }

    private bool HasEatenAll() 
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    public void PlayerDeath()
    {
        pacman.gameObject.SetActive(false);

        SetLives(currentLives - 1);

        if (currentLives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else 
        {
            GameOver();
        }
    }

}
