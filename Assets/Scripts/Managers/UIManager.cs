using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private TMP_Text _displayScoreText;

    [SerializeField] private TMP_Text _playerOneScoreText;
    [SerializeField] private TMP_Text _playerTwoScoreText;

    [SerializeField] private TMP_Text _playerOneTimerText;
    [SerializeField] private TMP_Text _playerTwoTimerText;

    private bool _showHelpPanel = false;
    private bool _gameOver = false;

    private void Update()
    {
        if (_gameOver) return;

        if (Input.GetKeyDown(KeyCode.H))
        {
            _showHelpPanel = !_showHelpPanel;
            _helpPanel.SetActive(_showHelpPanel);
        }
    }

    public void UpdatePlayerOneScoreText(int newScore)
    {
        _playerOneScoreText.text = newScore.ToString();
    }
    public void UpdatePlayerTwoScoreText(int newScore)
    {
        _playerTwoScoreText.text = newScore.ToString();
    }
    public void UpdatePlayerOneTimerText(int newTime)
    {
        _playerOneTimerText.text = newTime.ToString() + "s";
    }
    public void UpdatePlayerTwoTimerText(int newTime)
    {
        _playerTwoTimerText.text = newTime.ToString() + "s";
    }
    public void ShowGameOverPanel(PlayerBase winningPlayer)
    {
        _gameOver = true;
        _gameOverPanel.SetActive(_gameOver);

        _displayScoreText.text = $"Winner is player {winningPlayer.name} with score of {winningPlayer.PlayerScore}";
    }
    public void RestartGame()
    {
        //Just restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
