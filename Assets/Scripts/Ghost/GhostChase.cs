using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        ghostController.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        Dictionary<float, Vector2> distancesToTarget = new Dictionary<float, Vector2>();

        if (node != null && this.enabled && !ghostController.scared.enabled)
        {
            foreach (Vector2 position in node.availableDirections) 
            {
                distancesToTarget.Add(Vector2.Distance(new Vector3 (position.x, position.y) + ghostController.transform.position, ghostController.target.position), position);
            }
              
            Vector2 nextBestNode = distancesToTarget[distancesToTarget.Keys.Min()];
           
            this.ghostController.movement.SetDirection(nextBestNode);
        }
    }
}
