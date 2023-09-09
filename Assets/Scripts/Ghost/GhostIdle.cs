using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostIdle : GhostBehavior
{
    private void OnEnable()
    {
        ghostController.movement.setMotionless();
    }
    private void OnDisable()
    {
        ghostController.Body.enabled = true;
        if (ghostController.isDead)
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
        }
        ghostController.scatter.Enable();
    }

}
