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

    public GhostBehavior initialBehavior;

    public Transform target;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            if (scared.enabled)
            {
                GameManager.Instance.GhostDeath(this);
            }
            else 
            {
                GameManager.Instance.PlayerDeath();
            }
        }
    }
}
