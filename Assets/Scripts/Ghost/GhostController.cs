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
    public GhostReturn returnBehavior { get; private set; }
    //TODO
    public bool isDead = false;
    //Remplacer �a par un script qui renvoie le fantome � la base

    public GhostBehavior initialBehavior;

    public Transform target;

    public SpriteRenderer Body;
    public SpriteRenderer Blue;
    public SpriteRenderer White;

    public int points { get; private set; } = 200;

    private void Awake()
    {
        movement = GetComponent<Movement>();

        chase = GetComponent<GhostChase>();
        idle = GetComponent<GhostIdle>();
        scared = GetComponent<GhostScared>();
        scatter = GetComponent<GhostScatter>();
        returnBehavior = GetComponent<GhostReturn>();

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

        scared.Enable();

        Body.enabled = false;
        Blue.enabled = true;
        White.enabled = false;

        Invoke(nameof(NotScared), duration);
        Invoke(nameof(SoonNotScared), duration - 3);
    }

    public void NotScared()
    {
        CancelInvoke();

        Body.enabled = true;
        Blue.enabled = false;
        White.enabled = false;

        scared.Disable();
    }

    private void SoonNotScared()
    {
        Body.enabled = false;
        Blue.enabled = false;
        White.enabled = true;
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

                    returnBehavior.enabled = true;
                    NotScared();

                    Body.enabled = false;
                    Blue.enabled = false;
                    White.enabled = false;

                    GameManager.Instance.GhostDeath(this);

                }
                else
                {
                    GameManager.Instance.PlayerDeath();
                }
            }
        }
    }
}
