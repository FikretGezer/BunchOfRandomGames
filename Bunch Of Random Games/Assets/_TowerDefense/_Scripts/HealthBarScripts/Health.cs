using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Action<Health> OnHealthAdded = delegate{};
    public static Action<Health> OnHealthRemoved = delegate{};
    [HideInInspector] public float healthAmount = 100f;
    public Enemy _enemyScript;
    private void OnEnable() {
         _enemyScript = this.GetComponent<Enemy>();




        // healthAmount = 1f;
        OnHealthAdded(this);
    }
    private void OnDisable() {
        OnHealthRemoved(this);
    }
}
