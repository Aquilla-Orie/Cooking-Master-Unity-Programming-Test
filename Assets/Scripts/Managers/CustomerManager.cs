using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private Customer _customerObject;
    [SerializeField] private Table[] _tables;

    [SerializeField] private List<Customer> _customers;

    private int _numberOfCustomersToSpawn;
    private float _initialInvokeWaitTime = 2f;//Time before generate customer method is invocated, in seconds
    private float _repeatingInvokeInterval = 60f;//Interval between generate customer invocation, in seconds
    private List<Table> _freeTables;

    private void Awake()
    {
        _numberOfCustomersToSpawn = 0;
        _freeTables = new List<Table>();
        _customers = new List<Customer>();
    }

    private void Start()
    {
        //Start generating customers after _initialInvokeWaitTime and repeat every _repeatingInvokeInterval
        InvokeRepeating("GenerateCustomer", _initialInvokeWaitTime, _repeatingInvokeInterval);
    }

    //Generate customer on free table
    private void GenerateCustomer()
    {
        FindFreeTables();
        if (_freeTables.Count == 0) return;//Break out if there are no free tables

        NumberOfCustomersToSpawn();
        SpawnCustomersOnFreeTables();
    }

    private void SpawnCustomersOnFreeTables()
    {
        for (int i = 0; i < _numberOfCustomersToSpawn; i++)
        {
            //Pick a random free table and spawn customers there
            var table = _freeTables[Random.Range(0, _freeTables.Count)];
            var customer = Instantiate(_customerObject, table._customerPositionOnTable.transform);

            //Call customer constructor to setup manager
            customer.Init(this);

            _customers.Add(customer);

            table._customerOnTable = customer;
            customer._customerTable = table;

            //Find free tables again since one is taken
            FindFreeTables();
        }
    }

    //Pick free table to spawn customer on
    private void FindFreeTables()
    {
        _freeTables.Clear();
        _freeTables = _tables.Where(t => !t._customerOnTable ).ToList();
    }

    private void NumberOfCustomersToSpawn()
    {
        //Spawn customers based on number of free tables
        _numberOfCustomersToSpawn = Random.Range(1, _freeTables.Count);
    }


    //Remove customer from table
    public void RemoveCustomer(Customer customer)
    {
        if (_customers.Count <= 0) return;

        _customers.Remove(customer);
        customer.DestroySelf();
    }

    public void RemoveAllCustomers()
    {
        var customers = FindObjectsOfType<Customer>();

        foreach (var customer in customers)
        {
            RemoveCustomer(customer);
        }
    }
}
