using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Eat();
        }
    }

    private void Eat() 
    {
        GameManager.Instance.CherryEaten(this);
    }
}
