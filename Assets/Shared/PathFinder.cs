using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathFinder : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] float distanceRemainingTreshold;

    bool m_destinationReached;
    bool destinationReached
    {
        get
        {
            return m_destinationReached;
        }
        set
        {
            m_destinationReached = value;
            if (m_destinationReached)
            {
                if(OnDestinationReached!=null)
                    OnDestinationReached();
            }
        }
    }

    public event System.Action OnDestinationReached;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Vector3 target)
    {
        destinationReached = false;
        Agent.SetDestination(target);        
    }

    private void Update()
    {
        if (destinationReached)
            return;
        if(Agent.remainingDistance < distanceRemainingTreshold)
        {
            destinationReached = true;
        }
    }
}
