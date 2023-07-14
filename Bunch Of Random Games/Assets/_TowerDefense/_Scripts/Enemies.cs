using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public static List<Enemy> _enemyList = new List<Enemy>();
    public static float spawnObjectMovingSpeed = 1f;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform beginningPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private bool isCamShaking;

    private float _elapsedTime;
    private int count = 1;
    
    private Camera _mainCam;
    private Vector3 _beginPos;
    private void Awake() {
        _mainCam = Camera.main;
        _beginPos = _mainCam.transform.position;
    }   
    private void Update() {

        spawnObjectMovingSpeed = speed;
        if(CalculateTime(3f))
        {
            var spawnedEnemy = Instantiate(_enemy.gameObject, beginningPoint.position, Quaternion.identity);
            spawnedEnemy.name = "Enemy_" + count.ToString();
            count++;

            var agent = spawnedEnemy.GetComponent<NavMeshAgent>();
            if(agent != null)
            {
                agent.SetDestination(endPoint.position);
            }
        }
        if(isCamShaking)
            StartCoroutine(nameof(ShakeCamera));
        else
            StopCoroutine(nameof(ShakeCamera));

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
    IEnumerator ShakeCamera()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;

            Vector3 camPos = _beginPos + Random.insideUnitSphere;
            _mainCam.transform.position = camPos;

            yield return null; 
        }
    }
}
