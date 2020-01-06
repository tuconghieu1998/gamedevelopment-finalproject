﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController
{
    [SerializeField] float shootingSpeed;
    [SerializeField] float burstDurationMax;
    [SerializeField] float burstDurationMin;

    EnemyPlayer enemyPlayer;
    bool shouldFire;

    private void Start()
    {
        enemyPlayer = GetComponent<EnemyPlayer>();
        enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
    }

    private void EnemyPlayer_OnTargetSelected(Player target)
    {
        ActiveWeapon.AimTarget = target.transform;
        ActiveWeapon.AimTargetOffset = Vector3.up * 1.5f;
        StartBurst();
    }

    void StartBurst()
    {
        if (!enemyPlayer.EnemyHealth.isAlive)
            return;
        CheckReload();
        shouldFire = true;
        GameManager.Instance.Timer.Add(EndBurst, Random.Range(burstDurationMin, burstDurationMax));
    }

    void EndBurst()
    {
        shouldFire = false;
        if (!enemyPlayer.EnemyHealth.isAlive)
        {
            return;
        }
        CheckReload();
        GameManager.Instance.Timer.Add(StartBurst, shootingSpeed);
    }

    void CheckReload()
    {
        if (ActiveWeapon.reloader.RoundsRemainingInClip == 0)
        {
            ActiveWeapon.Reload();
        }
    }

    private void Update()
    {
        if (!shouldFire || !CanFire || !enemyPlayer.EnemyHealth.isAlive)
            return;
        ActiveWeapon.Fire();
    }
}
