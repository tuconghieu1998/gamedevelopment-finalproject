using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class WeaponRecoil : MonoBehaviour
{
    [System.Serializable]
    public struct Layer
    {
        public AnimationCurve curve;
        public Vector3 direction;
    }

    [SerializeField] Layer[] layers;
    [SerializeField] float recoilSpeed;
    [SerializeField] float recoilCooldown;
    [SerializeField] float strength;

    float nextRecoilCooldown;
    float recoilActiveTime;

    Shooter m_Shooter;
    Shooter Shooter
    {
        get
        {
            if(m_Shooter == null)
            {
                m_Shooter = GetComponent<Shooter>();
            }
            return m_Shooter;
        }
    }

    public void Activate()
    {
        nextRecoilCooldown = Time.time + recoilCooldown;
    }

    private void Update()
    {
        if(nextRecoilCooldown > Time.time)
        {
            //giữ nút bắn liên tục
            recoilActiveTime += Time.deltaTime;
            float percentage = recoilActiveTime / recoilSpeed;
            percentage = Mathf.Clamp01(percentage);

            Vector3 recoilAmount = Vector3.zero;
            for(int i = 0; i < layers.Length; i++)
            {
                recoilAmount += layers[i].direction * layers[i].curve.Evaluate(percentage);
            }
            this.Shooter.AimTargetOffset = Vector3.Lerp(Shooter.AimTargetOffset, Shooter.AimTargetOffset + recoilAmount, strength * Time.deltaTime);
        }
        else
        {
            //Không giữ nút bắn
            recoilActiveTime -= Time.deltaTime;
            if (recoilActiveTime < 0)
            {
                recoilActiveTime = 0;
            }
            if(recoilActiveTime == 0)
            {
                this.Shooter.AimTargetOffset = Vector3.zero;
            }
        }
    }
}
