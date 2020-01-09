using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Destructable
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] Ragdoll ragdoll;
    [SerializeField] Text textHealth;


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

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        textHealth.text = string.Format("HP : {0}", HitPointRemaining);
    }

    [ContextMenu("Test Die")]
    void TestDie()
    {
        Die();
    }
}
