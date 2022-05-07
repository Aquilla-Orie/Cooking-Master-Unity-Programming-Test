using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoard : MonoBehaviour
{
    private int _maxVeggieCapacity = 3; //Maximum number of veggies that can be on the board per time
    private int _currentNumberOfVeggiesOnBoard; //number of veggies on the board at the moment

    private bool _isChoppingBoardBusy; //Indicates if a player is currently using the chopping board
    private bool _isChoppingBoardFull;

    private Stack<VeggieType> _veggiesOnChoppingBoard;


    public bool IsChoppingBoardBusy { get => _isChoppingBoardBusy; set => _isChoppingBoardBusy = value; }
    public bool IsChoppingBoardFull { get => _isChoppingBoardFull; private set => _isChoppingBoardFull = value; }

    private void Awake()
    {
        IsChoppingBoardBusy = false;
        IsChoppingBoardFull = false;
        _currentNumberOfVeggiesOnBoard = 0;

        _veggiesOnChoppingBoard = new Stack<VeggieType>();
    }

    public void AddVeggieToBoard(Veggie veggie)
    {
        ++_currentNumberOfVeggiesOnBoard;
        if (_currentNumberOfVeggiesOnBoard >= _maxVeggieCapacity) IsChoppingBoardFull = true;

        _veggiesOnChoppingBoard.Push(veggie.GetVeggieType());
        Debug.Log($"Veggie {_veggiesOnChoppingBoard.Peek()} had been added to the chopping board");
    }

    public Stack<VeggieType> RemoveVeggiesFromBoard()
    {
        var tempHolder = Helper.CloneStack<VeggieType>(_veggiesOnChoppingBoard);
        _veggiesOnChoppingBoard.Clear();

        _currentNumberOfVeggiesOnBoard = 0;
        IsChoppingBoardFull = false;

        return tempHolder;
    }
}
