using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    protected float _moveSpeed = 2f;
    [SerializeField] protected Veggie _veggie;
    [SerializeField] protected ChoppingBoard _choppingBoard;

    [SerializeField] protected KeyCode _interactKey;//Button used by each player to interact
    [SerializeField] protected KeyCode _pickupFromChoppingBoardKey;//Button used by each player to pickup from the chopping board

    protected Queue<Veggie> _veggiesPickedUp;//Unprepared Veggies picked up side tables
    protected Stack<VeggieType> _veggiesPrepared;//Prepared Veggied picked up from chopping board

    private void Awake()
    {
        _veggiesPickedUp = new Queue<Veggie>();
        _veggiesPrepared = new Stack<VeggieType>();
    }

    private void Update()
    {
        MovePlayer();
        Interact();
    }

    public virtual void MovePlayer()
    {
        //Implement in player child class
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //is in range to pick up veggie
        if (collider.TryGetComponent<Veggie>(out _veggie))
        {
            Debug.Log($"{_veggie.GetVeggieType()} in range");
        }

        //is in range of chopping board
        if (collider.TryGetComponent<ChoppingBoard>(out _choppingBoard))
        {
            Debug.Log($"{_choppingBoard.name} in range");
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        //No longer in range of veggie
        if (collider.TryGetComponent<Veggie>(out _veggie))
        {
            _veggie = null;
        }
        //no longer range of chopping board
        if (collider.TryGetComponent<ChoppingBoard>(out _choppingBoard))
        {
            Debug.Log($"{_choppingBoard.name} no longer in range");
            _choppingBoard = null;
        }
    }

    public virtual void Interact()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            if (_veggie) InteractWithVegies();
            if (_choppingBoard) InteractWithChoppingBoard();
        }
        if (Input.GetKeyDown(_pickupFromChoppingBoardKey))
        {
            if (_choppingBoard) PickupFromChoppingBoard();
        }
    }

    private void PickupFromChoppingBoard()
    {
        _veggiesPrepared = _choppingBoard.RemoveVeggiesFromBoard();
    }

    private void InteractWithChoppingBoard()
    {
        //Do nothing if player has no veggie in hand, chopping board in use, or chopping board is full
        if (_veggiesPickedUp.Count == 0 || _choppingBoard.IsChoppingBoardBusy || _choppingBoard.IsChoppingBoardFull) return;

        StartCoroutine(DelayPlayerWhileChopping());
        _choppingBoard.AddVeggieToBoard(_veggiesPickedUp.Dequeue());

        IEnumerator DelayPlayerWhileChopping()
        {
            _choppingBoard.IsChoppingBoardBusy = true;
            _moveSpeed = 0;
            Debug.Log($"Player {name} can no longer move");
            yield return new WaitForSeconds(1f);
            _moveSpeed = 2f;
            _choppingBoard.IsChoppingBoardBusy = false;
            Debug.Log($"Player {name} can now move");
        }
    }

    private void InteractWithVegies()
    {
        //Allow the player pick veggie if available, has less than two, and is not same veggie

        if (_veggiesPickedUp.Count >= 2) return;

        if (_veggiesPickedUp.Count == 0 || _veggiesPickedUp.Last() != _veggie)
        {
            _veggiesPickedUp.Enqueue(_veggie);
            _veggie = null;

            Debug.Log($"{_veggiesPickedUp.Last().name} added to queue");
        }
        Debug.Log($"{_veggiesPickedUp.Count} elements in the queue");
    }
}
