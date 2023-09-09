using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStarting : GhostBehavior
{
    private void OnDisable()
    {
        ghostController.transform.position = GameManager.Instance.startTarget.position;
        ghostController.scatter.Enable();

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
}
