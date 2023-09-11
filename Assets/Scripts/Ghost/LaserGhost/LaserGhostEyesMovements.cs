using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserGhostEyesMovements : MonoBehaviour
{
    [SerializeField]
    private Sprite eyesSpriteUp;
    [SerializeField]
    private Sprite eyesSpriteDown;
    [SerializeField]
    private Sprite eyesSpriteLeft;
    [SerializeField]
    private Sprite eyesSpriteRight;

    [SerializeField]
    private SpriteRenderer eyes;

    private Vector2 lookDirection;

    private Movement movementScript;

    private void Start() {
        movementScript = GetComponent<Movement>();
        movementScript.directionChanged.AddListener(OnDirectionChanged);
    }

    private void OnEnable()
    {
        eyes.sprite = eyesSpriteRight;
        lookDirection = new Vector2 (1, 0);
    }

    private void OnDirectionChanged(Vector2 direction) {
        if (direction.x > Mathf.Abs(direction.y))
        {
            if (transform.position.y > 0)
            {
                eyes.sprite = eyesSpriteDown;
                lookDirection = new Vector2(0, -1);
            }
            else
            {
                eyes.sprite = eyesSpriteUp;
                lookDirection = new Vector2(0, 1);
            }
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (transform.position.x > 0)
            {
                eyes.sprite = eyesSpriteLeft;
                lookDirection = new Vector2(-1, 0);
            }
            else
            {
                eyes.sprite = eyesSpriteRight;
                lookDirection = new Vector2(1, 0);
            }
        }
    }

    public Vector2 GetLookDirection()
    {
        return lookDirection;
    }
}
