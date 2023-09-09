using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    private int returnPatience = 5; // 1/Probability of changing direction if pacman tries to brain the AI

    private void OnDisable()
    {
        if (!ghostController.scared.enabled)
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
           
            if(!(nextBestNode == -ghostController.movement.currentDirection))
            {
                returnPatience--;
                if(UnityEngine.Random.Range(0, returnPatience) == 0)
                {
                    this.ghostController.movement.SetDirection(nextBestNode);
                    returnPatience = 5;
                }
                else
                {
                    this.ghostController.movement.SetDirection(distancesToTarget[distancesToTarget.Keys.OrderBy(k => k).Skip(1).First()]);
                }
                
            }
            else
            {
                returnPatience = 5;
                this.ghostController.movement.SetDirection(distancesToTarget[distancesToTarget.Keys.OrderBy(k => k).Skip(1).First()]);
            }
                
        }
    }
}
