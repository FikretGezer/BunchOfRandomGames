using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyTypeScriptable _enemyType;
    private Transform _endDestination;
    private NavMeshAgent _agent;
    public HealthBarCastle castle;
    [HideInInspector] public float healthAmount;
     public float maxHealthAmount;
    private void OnEnable() {
        Enemies.AddEnemyToList(this);
    }
    private void OnDisable() {
        Enemies.RemoveEnemyFromList(this);
    }
    private void Awake() {
        //maxHealthAmount = _enemyType.enemyHealth;
        //maxHealthAmount = 50f;
        //healthAmount = maxHealthAmount;
        _agent = GetComponent<NavMeshAgent>();
        castle = FindObjectOfType<HealthBarCastle>();
        _endDestination = GameObject.FindGameObjectWithTag("EndDestination").transform;
        if(_endDestination != null)
            _agent.SetDestination(_endDestination.position);
    }
    private void Start() {
        healthAmount = maxHealthAmount;
    }
    private void Update() {
        if(ReachedDestinationOrGaveUp(_agent))
        {
            CameraScript.isCamShaking = true;

            if(castle != null)
            {
                castle.doesCastleGotHit = true;
            }
            Destroy(this.gameObject);
        }
        if(healthAmount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private bool ReachedDestinationOrGaveUp(NavMeshAgent navMeshAgent)
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
