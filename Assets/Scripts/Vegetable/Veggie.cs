using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veggie : MonoBehaviour
{

    [SerializeField] private VeggieType _veggieType;

    public VeggieType GetVeggieType()
    {
        return _veggieType;
    }
}

public enum VeggieType
{
    A,
    B,
    C,
    D,
    E,
    F
}