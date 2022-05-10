using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : PickupBase
{
    [SerializeField] private float _speedIncrease = 3.5f;
    [SerializeField] private float _timeInterval = 5f;

    public override void GivePlayerBonus()
    {
        PlayerForPickup.IncreaseSpeed(_speedIncrease, _timeInterval);

        //Destroy pickup
        Destroy(gameObject);
    }
}
