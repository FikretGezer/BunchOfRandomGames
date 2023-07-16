using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void OnEnable() {
        Enemies.AddEnemyToList(this);
    }
    private void OnDisable() {
        Enemies.RemoveEnemyFromList(this);
    }
    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update() {
        if(ReachedDestinationOrGaveUp(_agent))
        {
            Enemies.isCamShaking = true;
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
