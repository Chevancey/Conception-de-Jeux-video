using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        if (ghostController.chase.enabled == false)
            ghostController.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !ghostController.scared.enabled) 
        {
            //Debug.Log(true);
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -ghostController.movement.currentDirection && node.availableDirections.Count > 1) 
            {
                index++;
                if (index >= node.availableDirections.Count) 
                {
                    index = 0;
                }
            }

            Debug.Log(node.availableDirections[index]);
            this.ghostController.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
