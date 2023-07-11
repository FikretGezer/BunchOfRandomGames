using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public static List<Enemy> _enemyList = new List<Enemy>();
    
    private float _elapsedTime;
    private int count = 1;
    
    [SerializeField] private float speed = 1f;
    public static float spawnObjectMovingSpeed = 1f;

    private void Update() {

        spawnObjectMovingSpeed = speed;
        if(CalculateTime(3f))
        {
            Vector3 a = new Vector3(0,1,-4);
            var spawnedEnemy = Instantiate(_enemy.gameObject, Vector3.zero + a, Quaternion.identity);
            spawnedEnemy.name = "Enemy_" + count.ToString();
            count++;
        }
    }
    public static void AddEnemyToList(Enemy enemy)
    {
        if(!_enemyList.Contains(enemy))
            _enemyList.Add(enemy);
    }
    public static void RemoveEnemyFromList(Enemy enemy)
    {
        if(_enemyList.Contains(enemy))
            _enemyList.Remove(enemy);
    }
    private bool CalculateTime(float maxTime)
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > maxTime)
        {
            _elapsedTime = 0f;
            return true;
        }             
        else return false;   
    }
}
