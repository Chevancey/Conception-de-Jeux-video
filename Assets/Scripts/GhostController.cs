using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] bodySpriteRenderer;
    private int currentSprite = 0;
    private float timeBetweenFrames = 0.25f;

    private GameManager gameManager;

    private Movement movement;

    public bool isVulnerable { get; private set; } = false;
    //Use that to set the movement away from the player (ghosts are affraid)
    private bool isDead = false;
    //Use that to set the movement to the base


    public int points { get; private set; } = 200;

    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
    }


    void Update()
    {
        
    }

    public void SetVulnerable(float duration)
    {
        CancelInvoke();
        isVulnerable = true;

        currentSprite = 1;
        bodySpriteRenderer[1].enabled = true;
        bodySpriteRenderer[0].enabled = false;

        Invoke(nameof(SetInvulnerable), duration);
        InvokeRepeating(nameof(SetSoonInvulnerable), duration - 3, timeBetweenFrames);
    }

    private void SetInvulnerable()
    {
        CancelInvoke();
        bodySpriteRenderer[0].enabled = true;
        bodySpriteRenderer[currentSprite].enabled = false;
        currentSprite = 0;
        isVulnerable = false;
    }

    private void SetSoonInvulnerable()
    {
        bodySpriteRenderer[(currentSprite % 2) + 1].enabled = true;
        bodySpriteRenderer[currentSprite].enabled = false;

        currentSprite = (currentSprite % 2) + 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!isDead)
            {
                
                if (isVulnerable)
                {
                    isDead = true;
                    SetInvulnerable();
                    bodySpriteRenderer[currentSprite].enabled = false;
                    gameManager.GhostDeath(this);
                }
                else
                {
                    gameManager.PlayerDeath();
                }
            }
        }
    }

    public void ResetState()
    {
        movement.ResetState();
        //See how you can reset the behaviour of the ghosts
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

}
