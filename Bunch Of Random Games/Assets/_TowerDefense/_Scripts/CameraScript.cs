using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static bool isCamShaking;
    [SerializeField] [Range(0f, 3f)] private float maxShakeTime = 0.3f;
    [SerializeField] [Range(0f, 1f)] private float shakeStrength = 0.1f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private Vector3 limitationMin, limitationMax;
    private void Update() {
        if(isCamShaking)
        {
            StartCoroutine(ShakeCamera(maxShakeTime, shakeStrength));
        }
        CameraMovement();
        //CameraZooming();
        //CameraMovementLimit();
    }
    private void CameraMovement()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        var move = transform.right * hor + transform.up * ver;
        transform.Translate(move * movementSpeed * Time.deltaTime);
    }
    private void CameraMovementLimit()
    {
        var posLimit = transform.localPosition;
        posLimit.x = Mathf.Clamp(posLimit.x, limitationMin.x, limitationMax.x);
        posLimit.z = Mathf.Clamp(posLimit.z, limitationMin.z, limitationMax.z);

        transform.localPosition = posLimit;
    }
    private void CameraZooming()//Read article on unity page mouseScrollDelta
    {
        var pos = transform.position;
        pos.y += -Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, limitationMin.y, limitationMax.y);
        transform.position = pos;
    }
    IEnumerator ShakeCamera(float maxTime, float shakeStrength)
    {
        float elapsedTime = 0f;
        Vector3 beginningPos = transform.position;
        while(elapsedTime < maxTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = beginningPos + Random.insideUnitSphere * shakeStrength;

            yield return null;
        }
        transform.position = beginningPos;
        isCamShaking = false;
    }
}
//x = 10.5, 17
//z = -13, -15.3
//y = 14.75, 18.3
