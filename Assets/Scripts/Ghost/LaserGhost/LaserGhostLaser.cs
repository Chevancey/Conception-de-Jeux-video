using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LaserGhostLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    public LayerMask layerMaskObstacle;
    public LayerMask layerMaskPacman;

    [SerializeField] private float preShotTime = 1.5f;
    private bool shoot = false;
    private Vector2 shotDirection;
    [SerializeField] private float mapSize = 32f;

    [SerializeField] private AudioClip[] audioClips;

    private void OnEnable()
    {
        SoundManager.Instance?.playClipAtPosition(transform.position, audioClips[1]);
        Invoke(nameof(StartShooting), preShotTime);
    }

    private void StartShooting()
    {
        SoundManager.Instance?.playClipAtPosition(transform.position, audioClips[0]);
        shoot = true;
    }

    private void Update()
    {
        if (shoot)
        {
            ShootLaser();
        }
    }

    public void EndShooting()
    {
        shoot = false;
        _lineRenderer.enabled = false;
        CancelInvoke();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        shoot = false;
        _lineRenderer.enabled = false;
        CancelInvoke();
        gameObject.SetActive(false);
    }

    private void ShootLaser() 
    {
        Vector3 endLaser = ((Vector2)transform.position + shotDirection * mapSize);
        endLaser.z = -1;
        DrawLaser(transform.position, endLaser);
        _lineRenderer.enabled = true;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, shotDirection, mapSize, layerMaskPacman);

        if(hits.Length > 0)
        {
            shoot = false;
            GameManager.Instance.PlayerDeath();
        }

    }

    private void DrawLaser(Vector2 startPos, Vector2 endPos)
    {
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
    }

    public void SetShotDirection(Vector2 direction)
    {
        shotDirection = direction;
    }
}
