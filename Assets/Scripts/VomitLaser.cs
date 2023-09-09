using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitLaser : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private Transform _transform;

    public LayerMask layerMask;

    void Awake()
    {
        _transform = GetComponent<Transform>();
        _lineRenderer = GetComponent<LineRenderer>();   
    }

    private void Update()
    {
        ShootVomit();
    }

    void ShootVomit() 
    {

        RaycastHit2D hit = Physics2D.Raycast(_transform.position, transform.right, float.PositiveInfinity, layerMask);

        Debug.DrawLine(_transform.position, hit.point);

        if (hit.collider != null) 
        {
            DrawVomit(_transform.position, hit.point + Vector2.right);
        }
    }

    private void DrawVomit(Vector2 startPos, Vector2 endPos)
    {
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
    }
}
