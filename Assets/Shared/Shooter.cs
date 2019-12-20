using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;

    private WeaponReloader reloader;

    float nextFireAllowed;
    public bool canFire;
    Transform muzzle;

    void Awake()
    {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();

        transform.SetParent(hand);
    }

    public void Reload()
    {
        if(reloader == null)
        {
            return;
        }
        reloader.Reload();
    }

    public virtual void Fire()
    {       
        canFire = false;

        if(Time.time < nextFireAllowed)
        {
            return;
        }

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundsRemainingInClip == 0)
                return;

            reloader.TakeFromClip(1);
        }
        nextFireAllowed = Time.time + rateOfFire;

        // instantiate the projectile
        Instantiate(projectile, muzzle.position, muzzle.rotation);

        canFire = true;
    }
}
