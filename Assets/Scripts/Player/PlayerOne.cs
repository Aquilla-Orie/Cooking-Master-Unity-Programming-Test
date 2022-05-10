using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : PlayerBase
{
    public override void MovePlayer()
    {
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -_moveSpeed * Time.deltaTime * Vector3.right;
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += _moveSpeed * Time.deltaTime * Vector3.right;
        }
        //Up
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += _moveSpeed * Time.deltaTime * Vector3.up;
        }
        //Down
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -_moveSpeed * Time.deltaTime * Vector3.up;
        }

    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void DeductPointsAllPlayers()
    {
        base.DeductPointsAllPlayers();
    }

    public override void UpdateScoreText()
    {
        base.UpdateScoreText();
        _uiManager.UpdatePlayerOneScoreText(PlayerScore);
    }

    public override void UpdateTimerText(int timeLeft)
    {
        base.UpdateTimerText(timeLeft);
        _uiManager.UpdatePlayerOneTimerText(timeLeft);
    }
}
