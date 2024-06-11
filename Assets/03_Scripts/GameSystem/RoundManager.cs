using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Slider _TimerUI;
    [SerializeField]
    private float _roundTimer;
    private float _currentTime;
    public static RoundManager Instance = null;

    [SerializeField]
    private EnemyCounter _enemyCounter;

    [SerializeField]
    private int[] leveling, _timer;
    private int _indexer = 0;

    private void StartTimer()
    {
        _currentTime = _roundTimer;
    }
    private void SetTimer()
    {
        _TimerUI.value = Mathf.InverseLerp(0, _roundTimer, _currentTime);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StartTimer();
    }
    private void FixedUpdate()
    {
        _currentTime -= Time.deltaTime;
        if(_currentTime < 0)
        {
            //NextWave();
        }
        SetTimer();
    }

    private void NextWave()
    {
        _enemyCounter.ResetCount();
        _enemyCounter.EnemyLimit = leveling[_indexer];
        _roundTimer = _timer[_indexer];
        StartTimer();
        _indexer++;
    }
}
