using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrototypeEnemyBrain : MonoBehaviour
{
    [SerializeField]
    private Transform destination;
    private NavMeshAgent agent;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
    }
}
