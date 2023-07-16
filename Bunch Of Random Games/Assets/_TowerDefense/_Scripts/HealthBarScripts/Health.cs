using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Action<Health> OnHealthAdded = delegate{};
    public static Action<Health> OnHealthRemoved = delegate{};
    private void OnEnable() {
        OnHealthAdded(this);
    }
    private void OnDisable() {
        OnHealthRemoved(this);
    }
    
}
