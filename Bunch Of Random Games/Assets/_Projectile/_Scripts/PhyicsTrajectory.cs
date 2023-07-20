using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyicsTrajectory : MonoBehaviour
{
    [SerializeField] private int numPoints = 50;
    [SerializeField] private float timeBetweenPoints = 0.1f;
    [SerializeField] private float blastPower = 15f;
    [SerializeField] private LayerMask collidableLayers;

    private LineRenderer _lineRenderer;
    private void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update() {
        DrawLine();
    }
    private void DrawLine()
    {
        _lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPos = transform.position;
        Vector3 startingVel = transform.up * blastPower;

        for (float t = 0; t < numPoints; t+=timeBetweenPoints)
        {
            Vector3 newPoint = startingPos + t * startingVel;
            newPoint.y = startingPos.y + startingVel.y * t + Physics.gravity.y/2f * t * t;
            points.Add(newPoint);

            if(Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
            {
                _lineRenderer.positionCount = points.Count;
                break;
            }
        }
        _lineRenderer.SetPositions(points.ToArray());
    }
}
