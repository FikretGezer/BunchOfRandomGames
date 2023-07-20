using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField] private float initialVelocity;
    [SerializeField] private float _angle;
    private Vector3 point;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float angle = _angle * Mathf.Deg2Rad;
            StopAllCoroutines();
            StartCoroutine(CoroutineMovement(initialVelocity, angle));
        }
        
    }
    IEnumerator CoroutineMovement(float v0, float angle)
    {
        float t = 0f;
        while(t < 100)
        {
            point = PointFunc(v0, angle, t);
            transform.position = point;
            t += Time.deltaTime;
            yield return null;
        }
    }
    private Vector3 PointFunc(float v0, float angle, float t)
    {
        float x = v0 * t * Mathf.Cos(angle);
        float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * t * t;
        return new Vector3(x, y, 0f);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point , 0.1f);
    }
}
