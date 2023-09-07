using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] AudioClip muchSound1;
    [SerializeField] AudioClip muchSound2;
    static bool munchingAlternate = false;

    public int points = 10;

    protected virtual void Eat() 
    {
        FindObjectOfType<GameManager>().PelletEaten(this);

        if (munchingAlternate)
            SoundManager.Instance?.playClipAtPosition(transform.position, muchSound1);
        else
            SoundManager.Instance?.playClipAtPosition(transform.position, muchSound2);

        munchingAlternate = !munchingAlternate;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            Eat();
        }
    }
}
