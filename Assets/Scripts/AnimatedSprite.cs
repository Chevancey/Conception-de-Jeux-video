using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;

    public float stepWait = 0.25f;
    public int animationFrame { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(NextFrame), stepWait, stepWait);
    }

    private void NextFrame() 
    {
        if (!spriteRenderer.enabled) 
        {
            return;
        }
        animationFrame++;

        if (animationFrame >= sprites.Length && loop) 
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    public void Restart() 
    {
        animationFrame = -1;
        NextFrame();
    }

}
