using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructable
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] Ragdoll ragdoll;

    void SpawnAtNewSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    }

    public override void Die()
    {
        base.Die();
        ragdoll.EnableRagdoll(true);
        //SpawnAtNewSpawnPoint();
    }

    [ContextMenu("Test Die")]
    void TestDie()
    {
        Die();
    }
}
