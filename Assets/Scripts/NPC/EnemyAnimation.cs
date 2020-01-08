using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    Vector3 lastPosition;
    PathFinder pathFinder;
    EnemyPlayer enemyPlayer;

    private bool m_IsCrouched;
    public bool IsCrouched
    {
        get
        {
            return m_IsCrouched;
        }
        internal set
        {
            m_IsCrouched = true;
            GameManager.Instance.Timer.Add(CheckIsSaveToStandUp, 25);
        }
    }
    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        enemyPlayer = GetComponent<EnemyPlayer>();
    }

    private void Update()
    {
        float velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
        lastPosition = transform.position;
        animator.SetBool("IsWalking", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE);
        animator.SetFloat("Vertical", velocity / pathFinder.Agent.speed);
        animator.SetBool("IsAiming", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.AWARE);
        animator.SetBool("IsCrouched", IsCrouched);
    }

    void CheckIsSaveToStandUp()
    {
        bool isUnaware = enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE;
        if (isUnaware)
            IsCrouched = false;
    }
}
