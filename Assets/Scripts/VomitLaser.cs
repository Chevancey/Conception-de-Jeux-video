using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class VomitLaser : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private GhostController _ghostController;

    public LayerMask layerMaskObstacle;
    public LayerMask layerMaskEnemies;

    [SerializeField] private float shotTime = 0.5f;

    [SerializeField]
    private AudioClip[] audioClips;

    private void OnEnable()
    {
        SoundManager.Instance?.playClipAtPosition(transform.position, audioClips[0]);
        SoundManager.Instance?.playClipAtPosition(transform.position, audioClips[1]);
        _lineRenderer = GetComponent<LineRenderer>();
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
        SoundManager.Instance?
        gameObject.SetActive(false);
    }


    void ShootVomit() 
    {

        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, transform.right, float.PositiveInfinity, layerMaskObstacle);

        if (hitObstacle.collider != null)
        {
            DrawVomit(transform.position, hitObstacle.point);

            float distanceToObstacle = Vector2.Distance(transform.position, hitObstacle.point);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, distanceToObstacle, layerMaskEnemies);

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
