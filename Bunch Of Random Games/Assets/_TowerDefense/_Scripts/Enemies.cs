using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public static List<Enemy> _enemyList = new List<Enemy>();
    public static float spawnObjectMovingSpeed = 1f;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float enemyMovingSpeed = 1f;
    [SerializeField] private Transform enenmyBeginningPoint;
    [SerializeField] private Transform enemyDestinationPoint;

    private Camera _mainCam;
    private Vector3 _beginPos;
    private int unitCount = 1;  
    private float _elapsedTime = 0f;
    private GameObject parentOfEnemies;

    private void Awake() {
        _mainCam = Camera.main;
        _beginPos = _mainCam.transform.position;
        parentOfEnemies = new GameObject();
        parentOfEnemies.name = "Parent Of Enemies";
    }   
    private void Update() {

        spawnObjectMovingSpeed = enemyMovingSpeed;
        if(CalculateTime(3f))
        {
            var spawnedEnemy = Instantiate(_enemy.gameObject, enenmyBeginningPoint.position, Quaternion.identity);
            spawnedEnemy.name = "Enemy_" + unitCount.ToString();
            spawnedEnemy.transform.parent = parentOfEnemies.transform;
            unitCount++;

            var agent = spawnedEnemy.GetComponent<NavMeshAgent>();
            if(agent != null)
            {
                agent.SetDestination(enemyDestinationPoint.position);
            }            
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
        return false;
    }
}
