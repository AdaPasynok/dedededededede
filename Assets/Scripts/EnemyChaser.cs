using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : Enemy
{
    private NavMeshAgent navMeshAgent;

    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        OnPlayerDetected += ChasePlayer;
    }
    private void ChasePlayer(Vector3 playerPosition)
    {
        if (isActive)
        {
            NavMeshPath path = new();
            navMeshAgent.CalculatePath(playerPosition, path);

            if (path.status == NavMeshPathStatus.PathComplete)
            {
                navMeshAgent.path = path;
            }
        }
    }

    public override void OnGotShot(GameObject objectShot, Vector3 hitPoint, Vector3 direction)
    {
        if (!isDead)
        {
            navMeshAgent.isStopped = true;
            base.OnGotShot(objectShot, hitPoint, direction);
        }
    }
}
