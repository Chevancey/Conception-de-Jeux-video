using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GhostReturn : GhostBehavior
{
    private void OnEnable()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnDisable()
    {
        ghostController.idle.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        Dictionary<float, Vector2> distancesToTarget = new Dictionary<float, Vector2>();

        if (node != null && this.enabled && !ghostController.scared.enabled)
        {
            foreach (Vector2 position in node.availableDirections)
            {
                distancesToTarget.Add(Vector2.Distance(new Vector3(position.x, position.y) + ghostController.transform.position, GameManager.Instance.startTarget.position), position);
            }

            Vector2 nextBestNode = distancesToTarget[distancesToTarget.Keys.Min()];

            if(!(nextBestNode == -ghostController.movement.currentDirection))
            {
                this.ghostController.movement.SetDirection(nextBestNode);
            }
            else
            {
                this.ghostController.movement.SetDirection(distancesToTarget[distancesToTarget.Keys.OrderBy(k => k).Skip(1).First()]);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("GhostStartPoint")) 
        {
            Debug.Log("ok");
            this.Disable();
        }


    }

}
