using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerName", menuName = "ScriptableObjects/Towers", order = 1)]
public class Towers : ScriptableObject
{
    public float towerShootingRate; //Higher it is, speed will be lower
    public float damageAmount;
    public Bullet ammoType;
}
