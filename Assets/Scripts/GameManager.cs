using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public GhostController[] _ghost;
    public GhostController[] ghost
    {
        get => _ghost;
    }

    public PlayerController pacman;
    private SpriteRenderer pacmanRenderer;

    [SerializeField] private Tilemap boundsTilemap;

    public Transform pellets;

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource music;

    public int currentScore { get; private set; }
    public int currentLives { get; private set; }
    public int pointMultiplier { get; private set; } = 1;

    void Start()
    {
        StatNewGame();
        pacmanRenderer = pacman.gameObject.GetComponent<SpriteRenderer>();
        foreach (GhostController ghosti in _ghost)
        {
            ghosti.gameManager = this;
        }
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
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowPelletEaten(PowPellet powPellet)
    {
        PelletEaten(powPellet);
        if (!HasEatenAll())
        {
            CancelInvoke();
            foreach(GhostController ghosti in _ghost)
            {
                ghosti.SetVulnerable(powPellet.duration);
                // add ghosts being scared off in the ghostController
            }

            music.clip = audioClips[1];
            music.Play();
            pacmanRenderer.color = Color.green;
            boundsTilemap.color = Color.red;

            Invoke(nameof(EndPoweredState), powPellet.duration);
        }
    }

    public void EndPoweredState()
    {
        music.clip = audioClips[0];
        music.Play();
        pacmanRenderer.color = Color.white;
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
