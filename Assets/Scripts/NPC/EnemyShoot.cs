using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Extensions;

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

    void CrouchState()
    {
        bool takeCover = Random.Range(0, 3) == 0;
        if (!takeCover)
            return;
        float distanceToTarget = Vector3.Distance(transform.position, ActiveWeapon.AimTarget.position);
        if (distanceToTarget > 15)
        {
            enemyPlayer.GetComponent<EnemyAnimation>().IsCrouched = true;
        }
    }

    void StartBurst()
    {
        if (!enemyPlayer.EnemyHealth.isAlive && !CanSeeTarget())
            return;
        CheckReload();
        CrouchState();
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
        CrouchState();
        if (CanSeeTarget())
        {
            GameManager.Instance.Timer.Add(StartBurst, shootingSpeed);
        }
        
    }

    bool CanSeeTarget()
    {
        if (!transform.IsInLineOfSight(ActiveWeapon.AimTarget.position, 90, enemyPlayer.playerScanner.mask, Vector3.up))
        {
            enemyPlayer.ClearTargetAndScan();
            return false;
        }
        return true;
    }

    void CheckReload()
    {
        if (ActiveWeapon.reloader.RoundsRemainingInClip == 0)
        {
            CrouchState();
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
