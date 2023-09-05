using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.XR;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    public Rigidbody2D my_rigidbody { get; private set; }

    public float speed = 8.0f;
    public float speedMuliplier = 1.0f;

    public Vector2 initialDirection;

    public LayerMask obstacleLayer;

    public Vector2 currentDirection { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startPosition { get; private set; }

    void Awake()
    {
        my_rigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    void Update() 
    {
        if (nextDirection != Vector2.zero) 
        {
            SetDirection(nextDirection);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = my_rigidbody.position;
        Vector2 translation = currentDirection * speed * speedMuliplier * Time.fixedDeltaTime;
        my_rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool noclip = false)
    {
        if (noclip || !isOccupide(direction))
        {
            currentDirection = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool isOccupide(Vector2 direciton)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direciton, 1.5f, obstacleLayer);

        return hit.collider != null;
    }

    public void ResetState() 
    {
        speedMuliplier = 1.0f;
        currentDirection = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startPosition;
        my_rigidbody.isKinematic = false;
        this.enabled = true;
    }


}
