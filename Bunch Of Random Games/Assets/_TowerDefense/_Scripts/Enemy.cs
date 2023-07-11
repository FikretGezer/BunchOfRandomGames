using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnEnable() {
        Enemies.AddEnemyToList(this);
    }
    private void OnDisable() {
        Enemies.RemoveEnemyFromList(this);
    }
    private void Update() {
        transform.Translate(Vector3.forward * Enemies.spawnObjectMovingSpeed * Time.deltaTime);
    }
}
