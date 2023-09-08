using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class PlayerController : MonoBehaviour
{

    public Movement movement {get; private set;}
    private SpriteRenderer pacmanRenderer;
    private AnimatedSprite animatedSprite;

    void Awake()
    {
        movement = GetComponent<Movement>();
        pacmanRenderer = GetComponent<SpriteRenderer>();
        animatedSprite = GetComponent<AnimatedSprite>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }

        float angle = Mathf.Atan2(movement.currentDirection.y, movement.currentDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void CanEatGhost()
    {
        pacmanRenderer.color = Color.green;
    }
    public void CannotEatGhost()
    {
        pacmanRenderer.color = Color.white;
    }
    public void Dying()
    {
        movement.PlayerDying();
        animatedSprite.Dying();
    }

    public void ResetState()
    {
        movement.ResetState();
        animatedSprite.Restart();
    }
}
