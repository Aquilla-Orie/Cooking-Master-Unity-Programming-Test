using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : PlayerBase
{
    private void Update()
    {
        MovePlayer();
    }
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
}
