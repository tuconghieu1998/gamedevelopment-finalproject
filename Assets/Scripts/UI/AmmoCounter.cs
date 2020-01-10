using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] Text text;

    PlayerShoot playerShoot;
    WeaponReloader reloader;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    private void HandleOnLocalPlayerJoined(Player player)
    {
        playerShoot = player.PlayerShoot;
        playerShoot.OnWeaponSwitch += HandleOnWeaponSwitch;
    }

    private void HandleOnWeaponSwitch(Shooter activeWeapon)
    {
        reloader = activeWeapon.reloader;
        reloader.OnAmmoChanged += HandleOnAmmoChanged;
        HandleOnAmmoChanged();
    }

    void HandleOnAmmoChanged()
    {
        print("Change");//tesst
        int amountInInventory = reloader.RoundsRemainingInInventory;
        int amountInClip = reloader.RoundsRemainingInClip;
        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLocalPlayerJoined -= HandleOnLocalPlayerJoined;
        playerShoot.OnWeaponSwitch -= HandleOnWeaponSwitch;
        reloader.OnAmmoChanged -= HandleOnAmmoChanged;
    }
}
