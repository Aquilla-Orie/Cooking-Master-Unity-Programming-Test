using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CustomerManager _customerManager;
    [SerializeField] private UIManager _uiManager;


    private List<PlayerBase> _players;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;

        _players = new List<PlayerBase>();
    }

    public void PlayerTimeExpired(PlayerBase _player)
    {
        //Disable player controls
        _player.CanPlay = false;

        //Remove player from list
        _players.Add(_player);

        //check if all players are done
        if (_players.Count < 2) return;

        //End the game
        EndGame();
    }

    private void EndGame()
    {
        //Disable customer manager. Stop spawning customers
        _customerManager.RemoveAllCustomers();
        _customerManager.enabled = false;

        //Compare scores
        var winningPlayer = _players.OrderBy(x => x.PlayerScore).Last();//Scores are arranged in ascending order

        //Display gameover screen
        _uiManager.ShowGameOverPanel(winningPlayer);
    }
}