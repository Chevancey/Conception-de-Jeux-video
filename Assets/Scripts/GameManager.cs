using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    public bool easyDifficulty = true;
    public bool intermediateDifficulty = false;
    public bool hardDifficulty = false;

    [SerializeField] private Difficulty Easy;
    [SerializeField] private Difficulty Intermediate;
    [SerializeField] private Difficulty Hard;

    public GhostController[] _ghost;
    public GhostController[] ghost
    {
        get => _ghost;
    }
    public LaserGhostController _laserGhost;
    public LaserGhostController laserGhost
    {
        get => _laserGhost;
    }

    public PlayerController pacman;

    [SerializeField] private Tilemap boundsTilemap;

    public Transform pellets;

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource music;

    public int currentScore { get; private set; }
    public int currentLives { get; private set; }
    public int pointMultiplier { get; private set; } = 1;

    private bool isDead = false;

    private float waitForReset = 4.0f;

    public Transform startTarget;

    [SerializeField] private CherrySpawn cherrySpawn;

    void Start()
    {
        StartNewGame();
        for (int i = 0; i < ghost.Length; i++)
        {
            _ghost[i].target = pacman.transform;
        }
    }

    private void StartNewGame() 
    {
        SetScore(0);
        SetLives(3);
        SetupDifficulty();
        StartCoroutine(StartAfterSound(audioClips[3],NewRound));
    }

    private void GameOver() 
    {
        Invoke(nameof(ShowEndScreen), waitForReset);
    }

    void ShowEndScreen()
    {
        PlayerPrefs.SetInt(scoreKey, currentScore);
        SceneManager.LoadScene("HighScoreScene");
    }

    private void NewRound() 
    {
        foreach (Transform pellet in pellets) 
        { 
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private IEnumerator StartAfterSound(AudioClip sound,FunctionAfterSound func, float timeToWait=0) {
        yield return new WaitForSeconds(timeToWait);
        AudioClip previousSound = music.clip;
        music.clip = sound;
        music.loop = false;
        music.Play();
        Time.timeScale = 0;
        while(music.isPlaying) {
            yield return null;
        }
        Time.timeScale = 1;
        func();
    }

    private void ResetState() 
    {
        isDead = false;
        music.clip = audioClips[0];
        music.loop = true;
        music.Play();
        for (int i = 0; i < ghost.Length; i++)
        {
            _ghost[i].gameObject.SetActive(true);
            _ghost[i].ResetState();
        }
        if (hardDifficulty)
        {
            _laserGhost.gameObject.SetActive(true);
            _laserGhost.ResetState();
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
            foreach (GhostController ghost in _ghost)
            {
                ghost.gameObject.SetActive(false);
            }
            if (hardDifficulty)
            {
                _laserGhost.gameObject.SetActive(false);
            }

            pacman.movement.setMotionless();

            if (boundsTilemap.color == Color.red)
            {
                CancelInvoke();
                EndPoweredState();
            }
            StartCoroutine(StartAfterSound(audioClips[3],NewRound, waitForReset));
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

    public void CherryEaten(CherryItem cherry)
    {
        Destroy(cherry.gameObject);
        pacman.CanShoot();
        cherrySpawn.SetSpwanable();
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
        if (!isDead)
        {
            isDead = true;
            for (int i = 0; i < ghost.Length; i++)
            {
                _ghost[i].gameObject.SetActive(false);
            }
            if (hardDifficulty)
            {
                _laserGhost.movement.setMotionless();
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
    private delegate void FunctionAfterSound();
    private readonly string scoreKey = "score";


    private void SetupDifficulty()
    {
        List<List<float>> parameters = new List<List<float>>();
        if (hardDifficulty)
        {
            parameters = new List<List<float>>() { Hard.blinkyIdleScatterChase, 
                Hard.PinkyIdleScatterChase, Hard.InkyIdleScatterChase, Hard.ClydeIdleScatterChase };
            _laserGhost.gameObject.SetActive(true);
        }
        else if (intermediateDifficulty)
        {
            parameters = new List<List<float>>() { Intermediate.blinkyIdleScatterChase,
                Intermediate.PinkyIdleScatterChase, Intermediate.InkyIdleScatterChase,
            Intermediate.ClydeIdleScatterChase};
        }
        else if (easyDifficulty)
        {
            parameters = new List<List<float>>() { Easy.blinkyIdleScatterChase,
                Easy.PinkyIdleScatterChase, Easy.InkyIdleScatterChase,
            Easy.ClydeIdleScatterChase};

        }
        else
        {
            Debug.Log("No Difficulty Set");
        }

        for (int i = 0; i < _ghost.Length; i++)
        {
            ghost[i].ModifyBehaviourDuration(ghost[i].idle, parameters[i][0]);
            ghost[i].ModifyBehaviourDuration(ghost[i].scatter, parameters[i][1]);
            ghost[i].ModifyBehaviourDuration(ghost[i].chase, parameters[i][2]);
        }
    }
}
