using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Slider _TimerUI;
    [SerializeField]
    private float _roundTimer;
    private float _currentTime;

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
        StartTimer();
    }
    private void FixedUpdate()
    {
        _currentTime -= Time.deltaTime;
        SetTimer();
    }
}
