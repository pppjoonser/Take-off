using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public int _enemyLimit;
    public int _enemyCount = 0;
    public int _currentEnemyCount = 0;

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
        _currentEnemyCount = value;
    }
    public int GetScore()
    {
        return _currentEnemyCount;
    }
    public int GetLimit()
    {
        return _enemyLimit;
    }

    public void ResetCount()
    {
        _currentEnemyCount = 0;
    }
}
