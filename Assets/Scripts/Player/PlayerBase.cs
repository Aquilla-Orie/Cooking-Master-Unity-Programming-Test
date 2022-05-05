using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    protected float _moveSpeed = 2f;
    [SerializeField] protected Veggie _veggie;

    [SerializeField] protected KeyCode _interactKey;//Button used by each player to interact

    protected Queue<Veggie> _veggiesPickedUp;

    private void Awake()
    {
        _veggiesPickedUp = new Queue<Veggie>();
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
            Debug.Log($"Veggie {_veggie.name}, of Type {_veggie.GetVeggieType()}");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //No longer in range of veggie
        if (collision.TryGetComponent<Veggie>(out _veggie))
        {
            _veggie = null;
        }
    }

    public virtual void Interact()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            if (_veggie) InteractWithVegies();
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
