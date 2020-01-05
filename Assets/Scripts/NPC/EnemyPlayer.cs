using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(Scanner))]
public class EnemyPlayer : MonoBehaviour
{
    PathFinder pathFinder;
    Scanner scanner;

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        scanner = GetComponent<Scanner>();
        scanner.OnTargetSelected += Scanner_OnTargetSelected;
    }

    private void Scanner_OnTargetSelected(Vector3 position)
    {
        pathFinder.SetTarget(position);
    }
}
