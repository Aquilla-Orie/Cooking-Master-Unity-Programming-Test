using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlate : MonoBehaviour
{
    private bool _hasVeggieOnPlate;
    private Veggie _veggieOnPlate;

    public bool HasVeggieOnPlate { get => _hasVeggieOnPlate; private set => _hasVeggieOnPlate = value; }

    //Accept veggie from player
    public void DropVeggieOnPlate(Veggie veggie)
    {
        if (HasVeggieOnPlate) return;

        _veggieOnPlate = veggie;
        HasVeggieOnPlate = true;
        Debug.Log($"Veggie {veggie.GetVeggieType()} is on the side plate");
    }

    //Return veggie to player
    public Veggie PickupVeggieFromPlate()
    {
        if (!HasVeggieOnPlate) return null;

        var temp = _veggieOnPlate;
        _veggieOnPlate = null;
        HasVeggieOnPlate = false;
        Debug.Log($"Veggie {temp.GetVeggieType()} removed from side plate");
        return temp;
    }
}
