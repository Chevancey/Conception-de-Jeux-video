using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public Movement movement { get; private set; }

    public GhostChase chase { get; private set; } 
    public GhostIdle idle { get; private set; }
    public GhostScared scared { get; private set; }
    public GhostScatter scatter { get; private set; }
    //TODO
    private bool isDead = false;
    //Remplacer ça par un script qui renvoie le fantome à la base

    public GhostBehavior initialBehavior;

    public Transform target;

    [SerializeField] private SpriteRenderer[] bodySpriteRenderer;
    private int currentSprite = 0;
    private float timeBetweenFrames = 0.25f;

    public int points { get; private set; } = 200;



    private void Awake()
    {
        movement = GetComponent<Movement>();

        chase = GetComponent<GhostChase>();
        idle = GetComponent<GhostIdle>();
        scared = GetComponent<GhostScared>();
        scatter = GetComponent<GhostScatter>();

        //Debug.Log(initialBehavior == GetComponent <GhostScatter>());
    }

    private void Start() 
    {
        ResetState();
    }

    public void ResetState() 
    {
        gameObject.SetActive(true);
        movement.ResetState();

        scared.Disable();
        chase.Disable();
        scatter.Enable();

        if (idle != initialBehavior) 
        {
            idle.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    public void SetScared(float duration)
    {
        CancelInvoke();
        chase.Disable();
        scatter.Enable(); // TODO: replace with the scared
        scared.Enable();

        bodySpriteRenderer[currentSprite].enabled = false;
        currentSprite = 1;
        bodySpriteRenderer[currentSprite].enabled = true;

        Invoke(nameof(NotScared), duration);
        InvokeRepeating(nameof(SoonNotScared), duration - 3, timeBetweenFrames);
    }
    private void NotScared()
    {
        CancelInvoke();

        bodySpriteRenderer[currentSprite].enabled = false;
        currentSprite = 0;
        bodySpriteRenderer[currentSprite].enabled = true;

        scared.Disable();
        chase.Disable();
        scatter.Enable();

        if (idle != initialBehavior)
        {
            idle.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    private void SoonNotScared()
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
                if (scared.enabled)
                {
                    isDead = true;
                    NotScared();
                    bodySpriteRenderer[currentSprite].enabled = false;
                    GameManager.Instance.GhostDeath(this);

                    // TODO: behaviour once eaten
                }
                else
                {
                    GameManager.Instance.PlayerDeath();
                }
            }
        }
    }
}
