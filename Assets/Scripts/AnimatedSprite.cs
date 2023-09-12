using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    [SerializeField] private Sprite[] dyingSprites;

    public float stepWait = 0.125f;
    public float dyingStepWait = 0.2f;
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

    private void DyingFrame()
    {
        if (!spriteRenderer.enabled)
        {
            return;
        }
        animationFrame++;

        if (animationFrame >= 0 && animationFrame < dyingSprites.Length)
        {
            spriteRenderer.sprite = dyingSprites[animationFrame];
        }
        else if(animationFrame > dyingSprites.Length + 4)
        {
            spriteRenderer.sprite = null;
        }
    }

    public void Restart() 
    {
        CancelInvoke();
        animationFrame = -1;
        InvokeRepeating(nameof(NextFrame), stepWait, stepWait);
    }

    public void Dying()
    {
        CancelInvoke();
        animationFrame = -1;
        InvokeRepeating(nameof(DyingFrame), dyingStepWait, dyingStepWait);
    }

}
