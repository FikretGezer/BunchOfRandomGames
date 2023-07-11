using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shape : MonoBehaviour
{
    private Action<Shape> _killAction;
    public void Init(Action<Shape> killAction)
    {
        _killAction = killAction;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Ground")) _killAction(this);
    }
}
