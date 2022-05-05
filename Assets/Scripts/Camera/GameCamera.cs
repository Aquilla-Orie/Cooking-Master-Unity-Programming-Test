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

    [SerializeField] private float _playerDistanceOffset; //Compensate for small level size and smaller distances;

    private void Update()
    {
        //Pan camera based off distance between the two players
        float playerDistance = Vector2.Distance(_player1.position, _player2.position);
        float targetOrthSize = Mathf.Clamp(playerDistance + _playerDistanceOffset, _minOrthographicSize, _maxOrthographicSize);
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, targetOrthSize, Time.deltaTime);
    }
}
