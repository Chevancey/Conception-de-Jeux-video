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

    [SerializeField] private SpriteRenderer Body;
    [SerializeField] private SpriteRenderer Blue;
    [SerializeField] private SpriteRenderer White;

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
        scatter.Enable(); // TODO: replace with the scared
        scared.Enable();

        Body.enabled = false;
        Blue.enabled = true;
        White.enabled = false;

        Invoke(nameof(NotScared), duration);
        Invoke(nameof(SoonNotScared), duration - 3);
    }

    private void NotScared()
    {
        CancelInvoke();

        Body.enabled = true;
        Blue.enabled = false;
        White.enabled = false;

        Debug.Log("YEEEEEEEEEEEEEEE");

        scared.Disable();
        scatter.Enable();
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
                    NotScared();
                    Body.enabled = false;
                    Blue.enabled = false;
                    White.enabled = false;
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
