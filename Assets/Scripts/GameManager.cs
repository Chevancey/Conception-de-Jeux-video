using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{

    public GhostController[] _ghost;
    public GhostController[] ghost
    {
        get => _ghost;
    }

    public PlayerController pacman;

    [SerializeField] private Tilemap boundsTilemap;

    public Transform pellets;

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource music;

    public int currentScore { get; private set; }
    public int currentLives { get; private set; }
    public int pointMultiplier { get; private set; } = 1;

    private float waitForReset = 4.0f;

    public Transform startTarget;

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

        pacman.gameObject.SetActive(false);
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
        music.clip = audioClips[0];
        music.loop = true;
        music.Play();
        for (int i = 0; i < ghost.Length; i++)
        {
            _ghost[i].gameObject.SetActive(true);
            _ghost[i].ResetState();
        }

        pacman.gameObject.SetActive(true);
        pacman.ResetState();
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
        SetScore(currentScore + ghost.points * pointMultiplier);
        pointMultiplier *= 2;
    }

    private void ResetMultiplier() 
    {
        pointMultiplier = 1;
    }

    public void PelletEaten(Pellet pellet)
    {
        SetScore(currentScore + (pellet.points * pointMultiplier));

        pellet.gameObject.SetActive(false);

        if (HasEatenAll()) 
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), waitForReset);
        }
    }

    public void PowPelletEaten(PowPellet powPellet)
    {
        PelletEaten(powPellet);
        if (!HasEatenAll())
        {
            CancelInvoke();
            foreach(GhostController ghost in _ghost)
            {
                if (!ghost.isDead) 
                {
                    ghost.SetScared(powPellet.duration);
                }
            }


            music.clip = audioClips[1];
            music.Play();
            pacman.CanEatGhost();
            boundsTilemap.color = Color.red;

            Invoke(nameof(EndPoweredState), powPellet.duration);
        }
    }

    public void EndPoweredState()
    {
        music.clip = audioClips[0];
        music.Play();
        pacman.CannotEatGhost();
        boundsTilemap.color = Color.white;
        ResetMultiplier();
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
        for (int i = 0; i < ghost.Length; i++)
        {
            _ghost[i].gameObject.SetActive(false);
        }

        pacman.Dying();
        music.clip = audioClips[2];
        music.loop = false;
        music.Play();

        SetLives(currentLives - 1);

        if (currentLives > 0)
        {
            Invoke(nameof(ResetState), waitForReset);
        }
        else 
        {
            GameOver();
        }
    }

}
