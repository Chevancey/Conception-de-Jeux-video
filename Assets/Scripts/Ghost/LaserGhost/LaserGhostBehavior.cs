using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaserGhostController))]

public abstract class LaserGhostBehavior : MonoBehaviour
{
    public LaserGhostController laserGhostController {  get; private set; }
    public float setDuration;

    public void Awake()
    {
        laserGhostController = GetComponent<LaserGhostController>();
    }

    public void Enable()
    {
        Enable(setDuration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }

}
