using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLeaving : GhostBehavior
{
    private void OnEnable()
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)GameManager.Instance.startTarget.position) < 0.1)
        {
            this.Disable();
        }
        else
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            ghostController.movement.SetSpeed(4.0f);
            Vector2 direction = ((Vector2)GameManager.Instance.startTarget.position - (Vector2)transform.position).normalized;
            ghostController.movement.SetDirection(direction.normalized, true);
        }
    }

    private void OnDisable()
    {
        ghostController.isDead = false;
        gameObject.GetComponent<Collider2D>().isTrigger = false;

        int random = Random.Range(0, 2);
        if (random == 0)
        {
            this.ghostController.movement.SetDirection(Vector2.right);
        }
        else
        {
            this.ghostController.movement.SetDirection(Vector2.left);
        }

        if(ghostController.scared.enabled == false)
        {
            ghostController.Body.enabled = true;
            ghostController.movement.SetSpeed(7.0f);

            if (ghostController.initialBehavior == null)
            {
                ghostController.scatter.Enable();
            }
            else
            {
                ghostController.initialBehavior.Enable();
            }
        }           
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GhostStartingPoint"))
        {
            ghostController.movement.setMotionless();
            transform.position = GameManager.Instance.startTarget.position;
            this.Disable();
        }
    }
}
