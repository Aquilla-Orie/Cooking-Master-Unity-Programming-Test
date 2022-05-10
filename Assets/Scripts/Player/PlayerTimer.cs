using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    [SerializeField] private PlayerBase _player;
    [SerializeField] private float _waitingGrace = 1;//Time delay before player timer starts counting
    [SerializeField] private float _playerTime = 300f;//Both players start with 5 minutes time

    private void Start()
    {
        StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(_waitingGrace);//Delay the timer a bit before starting

        while (_playerTime >= 0f )
        {
            _playerTime -= Time.deltaTime;
            _player.UpdateTimerText((int)_playerTime);
            yield return null;
        }

        //Player time is over, inform GameManager
        GameManager.Instance.PlayerTimeExpired(_player);

        yield break;
    }

    public void BonusTime(float time)
    {
        _playerTime += time;
    }
}
