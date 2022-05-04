using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Transform _player1;
    [SerializeField] private Transform _player2;
    [SerializeField] private Camera _cam;

    private void Update()
    {
        float playerDistance = Vector2.Distance(_player1.position, _player2.position);
        _cam.orthographicSize = Mathf.Clamp(playerDistance, 4, 6);
    }
}
