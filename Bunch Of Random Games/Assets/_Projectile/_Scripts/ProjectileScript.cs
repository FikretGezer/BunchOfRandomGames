using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private Transform transformA, transformB, transformC;
    [SerializeField] private float sphereSize = 0.1f;
    [SerializeField] private float density = 0.1f;
    [SerializeField] [Range(0, 20)] private int realDensity = 20;
    [SerializeField] private float lerpSpeed = 0.1f;
    private float _current, _target;
    private Vector3 point;

    private void Update() {
        if(_current == 0f) _target = 1f;
        else if(_current == 1f) _target = 0f;
        _current = Mathf.MoveTowards(_current, _target, lerpSpeed * Time.deltaTime);
        point = Evaluate(_current);
    }
    private void OnDrawGizmos() {
        Vector3 pointA = transformA.position;
        Vector3 pointB = transformB.position;
        Vector3 pointC = transformC.position;


        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pointA, sphereSize);
        Gizmos.DrawSphere(pointB, sphereSize);
        Gizmos.DrawSphere(pointC, sphereSize);
        
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawLine(pointA, pointC);
        // Gizmos.DrawLine(pointB, pointC);
        // Gizmos.DrawLine(pointA, pointB);


        // Vector3 dir = (pointB - pointA).normalized;
        // float length = Vector3.Distance(pointA, pointB);
        // int pointCount = (int)(length / density);

        // float pos = 0f;
        // for (int i = 0; i < pointCount; i++)
        // {
        //     pos += density;
        //     Gizmos.DrawSphere(pointA + dir * pos, sphereSize);            
        // }

        /////////////////////////////////////////////////////////////////////////////

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(point, sphereSize);

        Gizmos.color = Color.red;
        for (int i = 1; i < realDensity; i++)
        {
            var pos = Evaluate(i / (float)realDensity);
            
            Gizmos.DrawWireSphere(pos, sphereSize);
            //Gizmos.DrawWireSphere(pos, sphereSize);
            
        }

    }
    private Vector3 Evaluate(float t)
    {
        Vector3 ac = Vector3.Lerp(transformA.position, transformC.position, t);
        Vector3 cb = Vector3.Lerp(transformC.position, transformB.position, t);
        return Vector3.Lerp(ac, cb, t);
    }
}
