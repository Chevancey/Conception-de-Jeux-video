using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserGhostScatter : LaserGhostBehavior
{
    [SerializeField] private LayerMask layerMaskPacman;
    [SerializeField] private LaserGhostEyesMovements eyeMovement;
    [SerializeField] private float shootingCoolDown = 10f;
    [SerializeField] private float fullShotDuration = 2f;
    [SerializeField] private float mapSize = 32f;
    [SerializeField] private LaserGhostLaser laser;
    private bool laserReady = false;
    private Vector2 laserDirection;
    private Vector2 directionBeforeShooting;


    private void OnEnable()
    {
        Invoke(nameof(ReadyShot),shootingCoolDown);
    }

    private void ReadyShot()
    {
        laserReady = true;
    }

    private void Update()
    {
        if (laserReady)
        {
            laserDirection = eyeMovement.GetLookDirection();
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, laserDirection, mapSize, layerMaskPacman);
            if (hits.Length > 0)
            {
                laserReady = false;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        directionBeforeShooting = laserGhostController.movement.currentDirection;
        laserGhostController.movement.setMotionless();
        laser.SetShotDirection(laserDirection);
        laser.gameObject.SetActive(true);
        Invoke(nameof(EndShooting), fullShotDuration);
    }

    private void EndShooting()
    {
        laser.EndShooting();
        laserGhostController.movement.SetDirection(directionBeforeShooting);
        Invoke(nameof(ReadyShot), shootingCoolDown);
    }

    private void OnDisable()
    {
        laser.EndShooting();
        laserReady = false;
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
