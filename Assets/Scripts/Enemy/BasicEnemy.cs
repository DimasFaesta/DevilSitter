using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyBase
{

    public Transform player;
    private void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        Jenis = "Basic";
        StartCoroutine(UpdateDestination());
    }

    private IEnumerator UpdateDestination()
    {
        for(int i = 0; i == i; i++)
        {
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(.5f);
        }

    }

}
