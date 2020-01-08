using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Destructable : MonoBehaviour
{
    [SerializeField] float hitPoints;

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    float damageTaken;

    public float HitPointRemaining
    {
        get
        {
            return hitPoints - damageTaken;
        }
    }

    public bool isAlive
    {
        get
        {
            return HitPointRemaining > 0;
        }
    }

    public virtual void Die()
    {
        //if (!isAlive)
        //{
        //    return;
        //}
        if (OnDeath != null)
        {
            OnDeath();
        }
    }

    public virtual void TakeDamage(float amount)
    {
        if (!isAlive)
            return;
        damageTaken += amount;
        if (OnDamageReceived != null)
        {
            OnDamageReceived();
        }
        if(HitPointRemaining <= 0)
        {
            Die();
        }
    }

    public void Reset()
    {
        damageTaken = 0;
    }
}
