using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : PlayerBase
{
    private void Update()
    {
        MovePlayer();
    }
    public override void MovePlayer()
    {
        //Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += -_moveSpeed * Time.deltaTime * Vector3.right;
        }
        //Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += _moveSpeed * Time.deltaTime * Vector3.right;
        }
        //Up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += _moveSpeed * Time.deltaTime * Vector3.up;
        }
        //Down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += -_moveSpeed * Time.deltaTime * Vector3.up;
        }

    }
}