using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Slider _waitingTimeSlider;

    private PlayerBase _playerServing;//Which player served the customer the plate
    private Stack<VeggieType> _plateServedByPlayer;

    private bool _hasBeenServed;
    private string _order;
    private int _maxOrderLength = 3; //max number of veggies per order a customer can generate
    private float _waitingTime; //Determined by order length
    private float _minWaitingTime;
    private float _maxWaitingTime;


    public Table _customerTable;

    private void Awake()
    {
        _hasBeenServed = false;
    }

    private void Start()
    {
        GenerateCustomerOrder();
        _customerTable.SetTableText(_order);

        CalculateWaitingTime();

        StartCoroutine(WaitingTimerCountdown());
    }

    private void CalculateWaitingTime()
    {
        string strippedOrder = _order.Replace(",", "");//Removing all commas from the string
        switch (strippedOrder.Length)
        {
            case 1:
                _maxWaitingTime = 45f;
                _minWaitingTime = 40f;
                break;
            case 2:
                _maxWaitingTime = 60f;
                _minWaitingTime = 55f;
                break;
            case 3:
                _maxWaitingTime = 80f;
                _minWaitingTime = 75f;
                break;
            default:
                break;
        }
        _waitingTime = UnityEngine.Random.Range(_minWaitingTime, _maxWaitingTime + 1);
    }

    private IEnumerator WaitingTimerCountdown()
    {
        float timer = _waitingTime;
        _waitingTimeSlider.maxValue = _waitingTime;

        while (timer >= 0f && !_hasBeenServed)
        {
            _waitingTimeSlider.value = timer;
            timer -= Time.deltaTime;
            yield return null;
        }

        //Customer is angry, has not been served within waiting time
        DeductBothPlayerPoints();
        DestroySelf();

        yield break;
    }


    private void GenerateCustomerOrder()
    {
        int veggiesOrdered = UnityEngine.Random.Range(1, _maxOrderLength + 1);//number of veggies to order

        //Get random veggie type as order
        var veggieTypes = Helper.GetDistinctEnumValues<VeggieType>(veggiesOrdered);

        _order = string.Join(",", veggieTypes);
    }

    public void DestroySelf()
    {
        //Clear table order string
        _customerTable.SetTableText(string.Empty);
        Destroy(gameObject);
    }

    public void DeductBothPlayerPoints()
    {
        var players = FindObjectsOfType<PlayerBase>();
        foreach (var player in players)
        {
            player.DeductPointsAllPlayers();
        }
    }

    public void ReceivePlateFromPlayer(PlayerBase player)
    {
        _playerServing = player;
        _plateServedByPlayer = _playerServing.GetVeggiePlate();
        _hasBeenServed = true;

        ComparePlates();
    }

    //Check if plate served is consistent with order made
    public void ComparePlates()
    {
        var plateArray = _plateServedByPlayer.ToArray();

        Array.Reverse(plateArray);//Reverse the stack to match the string
        var a = string.Join(",", plateArray);

        bool doOrdersMatch = string.Equals(a, _order);

        if (!doOrdersMatch)
        {
            //Punish player for serving wrong combination
            _playerServing.DeductPlayerScore();
            DestroySelf();
            return;
        }

        //Reward player for serving right combination
        _playerServing.AddPlayerScore();
        //Check if player served within time and add bonus


        DestroySelf();
    }

}
