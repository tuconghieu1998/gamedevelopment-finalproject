using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] WaypointController waypointController;
    [SerializeField] float waitTimeMin;
    [SerializeField] float waitTimeMax;

    PathFinder pathFinder;

    private void Start()
    {
        waypointController.SetNextWaypoint();
    }

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        pathFinder.OnDestinationReached += PathFinder_OnDestinationReached;
        waypointController.OnWaypointChanged += WaypointController_OnWaypointChanged;
    }

    private void WaypointController_OnWaypointChanged(Waypoint waypoint)
    {
        pathFinder.SetTarget(waypoint.transform.position);
    }

    private void PathFinder_OnDestinationReached()
    {
        GameManager.Instance.Timer.Add(waypointController.SetNextWaypoint, Random.Range(waitTimeMin, waitTimeMax));
        //StartCoroutine(SetNextWaypointAfterTime(Random.Range(waitTimeMin, waitTimeMax)));
    }
    //IEnumerator SetNextWaypointAfterTime(float time)
    //{

    //    yield return new WaitForSeconds(time);

    //    waypointController.SetNextWaypoint();
    //}
}
