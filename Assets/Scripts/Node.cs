using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirections { get; private set; }

    private void Start()
    {
        availableDirections = new List<Vector2>();

        CheckObstacles(Vector2.up);
        CheckObstacles(Vector2.down);
        CheckObstacles(Vector2.left);
        CheckObstacles(Vector2.right);
    }

    private void CheckObstacles(Vector2 direction) 
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstacleLayer);

        if (hit.collider == null) 
        {
            availableDirections.Add(direction);
        }
    }
}
