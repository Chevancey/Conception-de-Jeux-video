using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GhostController))]

public abstract class GhostBehavior : MonoBehaviour
{
    
    public GhostController ghostController {  get; private set; }
    public float setDuration;

    public void Awake()
    {
        ghostController = GetComponent<GhostController>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(setDuration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Enable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }

}
