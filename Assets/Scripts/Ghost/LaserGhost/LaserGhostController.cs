using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserGhostController : MonoBehaviour
{
    public Movement movement { get; private set; }
    public LaserGhostScatter scatter { get; private set; }

    public LaserGhostBehavior initialBehavior = null;

    private void Awake()
    {
        movement = GetComponent<Movement>();

        scatter = GetComponent<LaserGhostScatter>();

        //Debug.Log(initialBehavior == GetComponent <GhostScatter>());
    }

    private void Start() 
    {
        //ResetState();
    }

    public void ResetState() 
    {
        gameObject.SetActive(true);
        movement.ResetState();

        scatter.enabled = false;
        scatter.enabled = true;

    }

    public void SetScared(float duration)
    {
        //DoNothing;
    }
}
