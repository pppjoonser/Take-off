using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField]
    Slider TargetUI;

    public int _enemyLimit;
    public int _enemyCount = 0;
    private int _deadEnemy=0;
    public int _currentEnemyRespawnCount = 0;

    public static EnemyCounter Instance = null;

    public int EnemyLimit { get { return _enemyLimit; } set { _enemyLimit = value > 0 ? value : 0; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void EnemyIncrease(int value)
    {
        _currentEnemyRespawnCount = value;
        _enemyCount++;
    }
    public int GetScore()
    {
        return _currentEnemyRespawnCount;
    }
    public int GetLimit()
    {
        return _enemyLimit;
    }

    public void ResetCount()
    {
        _currentEnemyRespawnCount = 0;
    }

    public void EnemyDestroy()
    {
        _enemyCount--;
        _deadEnemy++;
        UIset(_deadEnemy);
    }

    private void UIset (int value)
    {
        TargetUI.value = Mathf.InverseLerp(0, EnemyLimit, EnemyLimit-value);
    }
}
