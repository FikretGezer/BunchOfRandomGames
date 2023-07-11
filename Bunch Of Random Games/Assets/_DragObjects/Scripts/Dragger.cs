using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Camera _cam;
    private Vector2 dragOffset;
    Vector2 screenPoint;
    private void Awake() {
        _cam = Camera.main;
        screenPoint = new Vector2(Screen.width, Screen.height);
    }
    private void OnMouseDown() {
        dragOffset = transform.position - GetPos();
    }
    private void OnMouseDrag() {
        transform.position = GetPos() + (Vector3)dragOffset;        
    }
    private Vector3 GetPos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        mousePos.x = Mathf.Clamp(mousePos.x, -screenPoint.x, screenPoint.x);
        mousePos.y = Mathf.Clamp(mousePos.y, -screenPoint.y, screenPoint.y);
        return mousePos;
    } 
}
