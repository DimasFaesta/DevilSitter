using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBase : MonoBehaviour
{
    public float Health;
    [HideInInspector]
    public string Jenis;
    [HideInInspector]
    protected NavMeshAgent agent;
}
