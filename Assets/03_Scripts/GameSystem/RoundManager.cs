using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Slider _TimerUI;
    private float _roundTimer;
    private float _currentTime;
    public static RoundManager Instance = null;
    [SerializeField] private GameObject _failUI;
    private SceneManagerScript _scene;

    [SerializeField]
    private EnemyCounter _enemyCounter;

    [SerializeField]
    private float[]  _timer;
    private int _indexer = 0;

    private void StartTimer()
    {
        _roundTimer = _timer[_indexer];
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
        _scene = FindObjectOfType<SceneManagerScript>();
    }
    private void FixedUpdate()
    {
        _currentTime -= Time.deltaTime;
        if(_currentTime < 0)
        {
            TimeOver();
        }
        SetTimer();
    }
    public void NextTimer()
    {
            _indexer++;
        if (_indexer < _timer.Length)
        {
            _TimerUI.value = 1;

            _roundTimer = _timer[_indexer];
            _currentTime = _roundTimer;
        }
    }
    private void TimeOver()
    {
        _scene.SetTime(0);
        _failUI.SetActive(true);
    }
}
