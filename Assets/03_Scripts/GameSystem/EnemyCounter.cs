using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EnemyCounter : MonoBehaviour
{
    [SerializeField]
    Slider TargetUI;

    [SerializeField]
    private GameObject _boss1;

    [SerializeField]
    private ScriptControler _scriptControler;

    private int _enemyLimit;
    public int _enemyCount = 0;
    private int _deadEnemy=0;
    public int _currentEnemyRespawnCount = 0;

    private RoundManager _roundManager;

    public static EnemyCounter Instance = null;

    private bool _dead = false;

    [SerializeField]
    private int[] _roundEnemyLimit;
    private int _indexer=0;

    private EnemyManager _enemyManager;

    public int EnemyLimit { get { return _enemyLimit; } set { _enemyLimit = value > 0 ? value : 0; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _roundManager = FindFirstObjectByType<RoundManager>();
        _enemyManager = GetComponentInChildren<EnemyManager>();
    }

    private void Start()
    {
        _enemyLimit = _roundEnemyLimit[_indexer];   
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
        if (_enemyCount <= 0)
        {
            NextRound();
        }
        UIset(_deadEnemy);
    }

    private void NextRound()
    {


        _indexer++;
        if (!_dead)
        {
            if (_indexer < _roundEnemyLimit.Length)
            {
                _enemyLimit = _roundEnemyLimit[_indexer];
                _roundManager.NextTimer();
                _deadEnemy = 0;
                _enemyCount = 0;
                _currentEnemyRespawnCount = 0;
                UIset(_deadEnemy);
            }
            else
            {
                _dead = true;
                _deadEnemy = 0;
                _enemyCount = 0;
                _currentEnemyRespawnCount = 0;
                UIset(_deadEnemy);
                BossCall();
            }
        }

        StartCoroutine(SetRespawn());
        StartCoroutine(TextOn());
    }

    private IEnumerator SetRespawn()
    {
        _dead = true;
        yield return new WaitForSeconds(4);
        _dead = false;
    }

    private IEnumerator TextOn()
    {
        yield return new WaitForSecondsRealtime(2f);
        _scriptControler.ShowTextBox();
    }

    private void BossCall()
    {
        _enemyManager.StopSpawn();
        _enemyManager.BossSpawn();
        
        
    }

    private void UIset (int value)
    {
        TargetUI.DOValue(Mathf.InverseLerp(0, EnemyLimit, EnemyLimit - value), 0.5f);


    }
}
