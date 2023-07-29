using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [HideInInspector] public int enemySpawnAmount;
    [HideInInspector] public int currentEnemyCount = 0;

    public static Enemies Instance;
    private void Awake() {
        if(Instance == null) Instance = this;

        _mainCam = Camera.main;
        _beginPos = _mainCam.transform.position;
        parentOfEnemies = new GameObject();
        parentOfEnemies.name = "Parent Of Enemies";
        currentEnemyCount = 0;
        IncreaseEnemyHealthByLevel();
    }  
    private void Start() {
        
        Debug.Log(SaveScript.Instance.GetLevel());
    } 
    private void Update() {

        spawnObjectMovingSpeed = enemyMovingSpeed;
        if(currentEnemyCount < enemySpawnAmount)
        {
            if(CalculateTime(3f))
            {
                currentEnemyCount++;
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
        // else if(currentEnemyCount >= enemySpawnAmount)
        // {
        //     if(HealthBarCastle.castleHealthFillAmount < 0.01f)
        //     {
        //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //     }
        //     else
        //     {
        //         var isThereEnemy = FindObjectOfType<Enemy>() ? true : false;
        //         var loadingScene = SceneManager.GetActiveScene().buildIndex + 1;
        //         if(loadingScene < SceneManager.sceneCountInBuildSettings && !isThereEnemy)
        //         {
        //             SaveScript.Instance.SaveLevel(SaveScript.Instance.GetLevel() + 1);
        //             SceneManager.LoadScene(loadingScene);
        //         }
        //     }
        // }
    }
    private void IncreaseEnemyHealthByLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        float castleHealth = 100f;
        switch(currentLevel)
        {
            case "Level 1":
                enemySpawnAmount = 10;
                _enemy.maxHealthAmount = 100f;
                castleHealth = 100f;
                break;
            case "Level 2":
                enemySpawnAmount = 12;
                _enemy.maxHealthAmount = 110f;
                castleHealth = 120f;
                break;
            case "Level 3":
                enemySpawnAmount = 13;
                _enemy.maxHealthAmount = 120f;
                castleHealth = 130f;
                break;
            case "Level 4":
                enemySpawnAmount = 14;
                _enemy.maxHealthAmount = 130f;
                castleHealth = 140f;
                break;
            case "Level 5":
                enemySpawnAmount = 15;
                _enemy.maxHealthAmount = 140f;
                castleHealth = 150f;
                break;
        }
        HealthBarCastle.maxHalthAmountOfCastle = castleHealth;
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
