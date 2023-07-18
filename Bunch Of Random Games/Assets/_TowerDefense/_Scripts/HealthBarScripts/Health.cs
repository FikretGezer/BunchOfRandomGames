using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Action<Health> OnHealthAdded = delegate{};
    public static Action<Health> OnHealthRemoved = delegate{};
    [HideInInspector] public float healthAmount = 1f;
    private void OnEnable() {
        healthAmount = 1f;
        OnHealthAdded(this);
    }
    private void OnDisable() {
        OnHealthRemoved(this);
    }
}
