using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Table : MonoBehaviour
{
    [SerializeField] private TMP_Text _tableText;

    public Customer _customerOnTable;
    public Transform _customerPositionOnTable;

    public void SetTableText(string text)
    {
        _tableText.text = text;
    }

    public void SetPlateForCustomer(PlayerBase player)
    {
        _customerOnTable.ReceivePlateFromPlayer(player);
    }
}
