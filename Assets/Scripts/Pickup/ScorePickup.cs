using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : PickupBase
{
    public override void GivePlayerBonus()
    {
        PlayerForPickup.AddBonusScore();

        //Destroy pickup after use
        Destroy(gameObject);
    }
}
