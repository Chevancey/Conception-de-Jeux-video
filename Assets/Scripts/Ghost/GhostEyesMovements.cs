using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostEyesMovements : MonoBehaviour
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

    private Movement movementScript;

    private void Start() {
        movementScript = GetComponent<Movement>();
        movementScript.directionChanged.AddListener(OnDirectionChanged);
    }

    private void OnDirectionChanged(Vector2 direction) {
        if (direction.x > 0 && direction.x > Mathf.Abs(direction.y)) {
            eyes.sprite = eyesSpriteRight;
        } else if (direction.x < 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ) {
            eyes.sprite = eyesSpriteLeft;
        } else if (direction.y > 0) {
            eyes.sprite = eyesSpriteUp;
        } else {
            eyes.sprite = eyesSpriteDown;
        }
    }
}
