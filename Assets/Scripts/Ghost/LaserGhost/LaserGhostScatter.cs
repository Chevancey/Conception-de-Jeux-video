using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class LaserGhostScatter : LaserGhostBehavior
{
    [SerializeField] private float minShootingCoolDown = 10f;
    [SerializeField] private float maxShootingCoolDown = 25;
    private LaserGhostLaser laser;


    private void OnEnable()
    {
        laser = GetComponentInChildren<LaserGhostLaser>();
        Invoke(nameof(Shoot),Random.Range(minShootingCoolDown,maxShootingCoolDown));
        Debug.Log("enable");
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
        laser.gameObject.SetActive(true);
        Invoke(nameof(Shoot), Random.Range(minShootingCoolDown, maxShootingCoolDown));
    }

    private void OnDisable()
    {
        laser.gameObject.SetActive(false);
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled) 
        {
            //Debug.Log(true);
            int index = Random.Range(0, node.availableDirections.Count);
            this.laserGhostController.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
