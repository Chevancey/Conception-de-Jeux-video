using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostScared : GhostBehavior
{
    private void OnEnable()
    {
        ghostController.chase.Disable();
        ghostController.scatter.Disable();

        ghostController.movement.SetSpeed(4f);
    }

    private void OnDisable()
    {
        if (!ghostController.idle.enabled)
            ghostController.scatter.Enable();

        ghostController.movement.SetSpeed(7f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        Dictionary<float, Vector2> distancesToTarget = new Dictionary<float, Vector2>();

        if (node != null && this.enabled && !ghostController.scatter.enabled && !ghostController.chase.enabled)
        {
            foreach (Vector2 position in node.availableDirections)
            {
                distancesToTarget.Add(Vector2.Distance(new Vector3(position.x, position.y) + ghostController.transform.position, ghostController.target.position), position);
            }

            Vector2 nextBestNode = distancesToTarget[distancesToTarget.Keys.Max()];

            this.ghostController.movement.SetDirection(nextBestNode);
        }
    }
}
