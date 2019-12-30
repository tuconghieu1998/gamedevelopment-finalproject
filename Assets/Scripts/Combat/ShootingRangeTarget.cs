using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeTarget : Destructable
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float repairTime;

    Quaternion initalRotation;
    Quaternion targetRotation;

    bool requiresRotation;

    private void Awake()
    {
        initalRotation = transform.rotation;
    }

    public override void Die()
    {
        base.Die();
        targetRotation = Quaternion.Euler(transform.right * 90);
        requiresRotation = true;
        GameManager.Instance.Timer.Add(() =>
        {
            targetRotation = initalRotation;
            requiresRotation = true;
        }, repairTime);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    private void Update()
    {
        if (!requiresRotation) 
            return;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (transform.rotation == targetRotation)
        {
            requiresRotation = false;
        }
    }
}
