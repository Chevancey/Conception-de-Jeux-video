using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LaserGhostLaser : MonoBehaviour
{
    private LaserGhostEyesMovements eyeMovement;
    private LineRenderer _lineRenderer;

    public LayerMask layerMaskObstacle;
    public LayerMask layerMaskPacman;

    [SerializeField] private float shotTime = 0.5f;

    [SerializeField]
    private AudioClip[] audioClips;

    private void OnEnable()
    {
        SoundManager.Instance?.playClipAtPosition(transform.position, audioClips[0]);
        SoundManager.Instance?.playClipAtPosition(transform.position, audioClips[1]);
        _lineRenderer = GetComponent<LineRenderer>();
        eyeMovement = GetComponentInParent<LaserGhostEyesMovements>();
        Invoke("StopShooting", shotTime);
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }


    private void Update()
    {
        ShootVomit();
    }

    private void StopShooting()
    {
        gameObject.SetActive(false);
    }


    void ShootVomit() 
    {

        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, eyeMovement.GetLookDirection(), float.PositiveInfinity, layerMaskObstacle);
        RaycastHit2D hitObstacle2 = Physics2D.Raycast(hitObstacle.transform.position, eyeMovement.GetLookDirection(), float.PositiveInfinity, layerMaskObstacle);

        if (hitObstacle2.collider != null)
        {
            DrawVomit(transform.position, hitObstacle2.point);

            float distanceToObstacle2 = Vector2.Distance(transform.position, hitObstacle2.point);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, eyeMovement.GetLookDirection(), distanceToObstacle2, layerMaskPacman);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    GhostController ghostController = hit.collider.gameObject.GetComponent<GhostController>();

                    if (ghostController != null && !ghostController.isDead)
                    {
                        ghostController.isDead = true;

                        ghostController.returnBehavior.enabled = true;
                        ghostController.NotScared();

                        ghostController.Body.enabled = false;

                        GameManager.Instance.GhostDeath(ghostController);
                    }
                }
            }
        }

    }

    private void DrawVomit(Vector2 startPos, Vector2 endPos)
    {
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
    }
}
