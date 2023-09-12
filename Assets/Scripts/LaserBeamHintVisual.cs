using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class LaserBeamHintVisual : MonoBehaviour
{
    bool show = false;
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(show == false && GameManager.Instance.pacman.canShoot)
        {
            StartBlinking();
        }else if (show == true && !GameManager.Instance.pacman.canShoot)
        {
            StopBlinking();
        }
    }

    void StopBlinking()
    {
        show = false;
        canvas.enabled = false;
    }

    void StartBlinking()
    {
        show = true;
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        while(show)
        {
            canvas.enabled = !canvas.enabled;
            yield return new WaitForSeconds(0.5f);
        }
        canvas.enabled = false;
    }
}
