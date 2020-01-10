using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    [SerializeField] Destructable[] targets;
    int targetsDestroyCounter;
    [SerializeField] Text textScore;
    [SerializeField]
    Text textTotal;

    private void Start()
    {
        for(int i = 0; i < targets.Length; i++)
        {
            targets[i].OnDeath += WinCondition_OnDeath;
        }

        textTotal.text = string.Format("Total: {0}", targets.Length);
    }

    private void WinCondition_OnDeath()
    {
        targetsDestroyCounter++;
        textScore.text = string.Format("Killed : {0}", targetsDestroyCounter);
        if (targetsDestroyCounter == targets.Length)
        {
            GameManager.Instance.EventBus.RaiseEvent("OnAllEnemiesKilled");
        }
    }
}
