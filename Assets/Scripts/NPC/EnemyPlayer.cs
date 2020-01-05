using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyPlayer : MonoBehaviour
{
    PathFinder pathFinder;
    [SerializeField] Scanner playerScanner;
    Player priorityTarget;
    List<Player> myTargets;

    [SerializeField] SwatSoldier settings;

    private EnemyHealth m_EnemyHealth;
    public EnemyHealth EnemyHealth
    {
        get
        {
            if(m_EnemyHealth == null)
            {
                m_EnemyHealth = GetComponent<EnemyHealth>();
            }
            return m_EnemyHealth;
        }
    }

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        pathFinder.Agent.speed = settings.RunSpeed;
        playerScanner.OnScanReady += Scanner_OnScanReady;
        Scanner_OnScanReady();
    }

    private void Scanner_OnScanReady()
    {
        if (priorityTarget != null)
            return;
        myTargets = playerScanner.ScanForTargets<Player>();
        if(myTargets.Count == 1)
        {
            priorityTarget = myTargets[0];
        }
        else
        {
            SelectClosestTarget();
        }
        if (priorityTarget != null)
        {
            SetDestinationToPriorityTarget();
        }
    }

    private void SetDestinationToPriorityTarget()
    {
        pathFinder.SetTarget(priorityTarget.transform.position);
    }

    private void SelectClosestTarget()
    {
        float closestTarget = playerScanner.ScanRange;
        foreach(var possibleTarget in myTargets)
        {
            if (Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
            {
                priorityTarget = possibleTarget;
            }
        }
    }
}
