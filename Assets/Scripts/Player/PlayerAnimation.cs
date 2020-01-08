using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    bool isInCover = false;

    private PlayerAim m_playerAim;
    public PlayerAim PlayerAim
    {
        get
        {
            if(m_playerAim == null)
            {
                m_playerAim = GameManager.Instance.LocalPlayer.playerAim;
            }
            return m_playerAim;
        }
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;
        animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);

        animator.SetBool("IsWalking", GameManager.Instance.InputController.isWalking);
        animator.SetBool("IsSprinting", GameManager.Instance.InputController.isSprinting);
        animator.SetBool("IsCrouched", GameManager.Instance.InputController.isCrouched);

        animator.SetFloat("AimAngle", PlayerAim.GetAngle());
        animator.SetBool("IsAiming", 
             GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
             GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING);
        animator.SetBool("IsInCover", GameManager.Instance.LocalPlayer.PlayerState.MoveState == PlayerState.EMoveState.COVER);
    }
}
