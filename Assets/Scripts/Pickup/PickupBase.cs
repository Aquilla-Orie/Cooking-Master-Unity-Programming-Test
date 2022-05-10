using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    public PlayerBase PlayerForPickup;//Player who this pickup is meant for

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerForPickup != collision.GetComponent<PlayerBase>()) return;

        GivePlayerBonus();
    }

    public virtual void GivePlayerBonus()
    {
        
    }
}
