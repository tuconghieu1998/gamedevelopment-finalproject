using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioReload;
    [SerializeField] AudioController audioFire;
    [SerializeField] public AudioController audioSwitchWeapon;

    public Transform AimTarget;
    public Vector3 AimTargetOffset;

    public WeaponReloader reloader;
    private ParticleSystem muzzleFireParticleSystem;

    private WeaponRecoil m_WeaponRecoil;
    private WeaponRecoil WeaponRecoil
    {
        get
        {
            if(m_WeaponRecoil == null)
            {
                m_WeaponRecoil = GetComponent<WeaponRecoil>();
            }
            return m_WeaponRecoil;
        }
    }

    float nextFireAllowed;
    public bool canFire;
    Transform muzzle;

    public void Equip()
    {        
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //audioSwitchWeapon.Play();
        //StartCoroutine(WaitForSound());
        //WaitForSeconds(audioSwitchWeapon.length);
        //print("Switch");
    }
    //public IEnumerator WaitForSound()
    //{
    //    yield return new WaitUntil(() => audioSwitchWeapon.isPlaying() == false);
    //    // or yield return new WaitWhile(() => audiosource.isPlaying == true)
    //}
    void Awake()
    {
        muzzle = transform.Find("Model/Muzzle");
        reloader = GetComponent<WeaponReloader>();
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();
    }

    public void Reload()
    {
        if(reloader == null)
        {
            return;
        }
        reloader.Reload();
        audioReload.Play();
    }

    void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;
        muzzleFireParticleSystem.Play();
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
        bool isLocalPlayerControlled = AimTarget == null;
        if (!isLocalPlayerControlled)
        {
            muzzle.LookAt(AimTarget.position + AimTargetOffset);
        }

        Projectile newBullet = (Projectile)Instantiate(projectile, muzzle.position, muzzle.rotation);
        if (isLocalPlayerControlled)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            Vector3 targetPosition = ray.GetPoint(500);
            if(Physics.Raycast(ray,out hit))
            {
                targetPosition = hit.point;
            }
            newBullet.transform.LookAt(targetPosition + AimTargetOffset);
        }
        if (this.WeaponRecoil)
        {
            this.WeaponRecoil.Activate();
        }

        FireEffect();
        // instantiate the projectile
        
        audioFire.Play();
        canFire = true;
    }
}
