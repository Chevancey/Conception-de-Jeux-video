using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowPellet : Pellet
{
    public float duration = 8.0f;

    protected override void Eat()
    {
        GameManager.Instance.PowPelletEaten(this);
    }
}
