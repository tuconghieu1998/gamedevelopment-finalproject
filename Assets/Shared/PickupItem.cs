using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag != "Player")
        {
            return;
        }
        PickUp(collider.transform);
    }

    private void PickUp(Transform item)
    {
        OnPickUp(item);
    }

    public virtual void OnPickUp(Transform item)
    {
        
    }
}
