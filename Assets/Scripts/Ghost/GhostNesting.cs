using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNesting : GhostBehavior
{
    private void OnEnable()
    {
        ghostController.movement.SetSpeed(4.0f);
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        ghostController.movement.SetDirection(ghostController.movement.startPosition - transform.position,true);
    }

    private void OnDisable()
    {
        ghostController.movement.SetSpeed(7.0f);
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        if(ghostController.scared.enabled == false)
        {
            ghostController.idle.Enable();
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, ghostController.movement.startPosition);
        if (distance < 0.1)
        {
            ghostController.movement.setMotionless();
            transform.position = ghostController.movement.startPosition;
            this.Disable();
        }
    }
}
