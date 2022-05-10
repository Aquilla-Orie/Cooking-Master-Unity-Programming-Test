using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickup : PickupBase
{
    [SerializeField] private float _bonusTime = 20f;

    public override void GivePlayerBonus()
    {
        PlayerForPickup.GetComponent<PlayerTimer>().BonusTime(_bonusTime);

        //Destroy Pickup
        Destroy(gameObject);
    }
}
