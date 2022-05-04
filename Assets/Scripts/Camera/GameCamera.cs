using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Transform _player1;
    [SerializeField] private Transform _player2;
    [SerializeField] private Camera _cam;

    [SerializeField] private float _minOrthographicSize = 4;
    [SerializeField] private float _maxOrthographicSize = 6;

    private void Update()
    {
        //Pan camera based off distance between the two players
        float playerDistance = Vector2.Distance(_player1.position, _player2.position);
        _cam.orthographicSize = Mathf.Clamp(playerDistance, _minOrthographicSize, _maxOrthographicSize);
    }
}
