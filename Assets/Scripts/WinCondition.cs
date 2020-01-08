using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] Destructable[] targets;
    int targetsDestroyCounter;

    private void Start()
    {
        for(int i = 0; i < targets.Length; i++)
        {
            targets[i].OnDeath += WinCondition_OnDeath;
        }
    }

    private void WinCondition_OnDeath()
    {
        targetsDestroyCounter++;
        if (targetsDestroyCounter == targets.Length)
        {
            GameManager.Instance.EventBus.RaiseEvent("OnAllEnemiesKilled");
        }
    }
}
