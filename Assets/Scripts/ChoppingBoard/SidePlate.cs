using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SidePlate : MonoBehaviour
{
    [SerializeField] private TMP_Text _sidePlateText;

    private bool _hasVeggieOnPlate;
    private Veggie _veggieOnPlate;

    public bool HasVeggieOnPlate { get => _hasVeggieOnPlate; private set => _hasVeggieOnPlate = value; }

    //Accept veggie from player
    public void DropVeggieOnPlate(Veggie veggie)
    {
        if (HasVeggieOnPlate) return;

        _veggieOnPlate = veggie;
        HasVeggieOnPlate = true;

        _sidePlateText.text = veggie.GetVeggieType().ToString();
    }

    //Return veggie to player
    public Veggie PickupVeggieFromPlate()
    {
        if (!HasVeggieOnPlate) return null;

        var temp = _veggieOnPlate;
        _veggieOnPlate = null;
        HasVeggieOnPlate = false;

        _sidePlateText.text = String.Empty;

        return temp;
    }
}
