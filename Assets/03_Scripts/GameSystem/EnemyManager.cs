using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _spawnPoint;
    [SerializeField] private GameObject _Boss;

    public float _enemySpawnFactor;
    private float _createMaxTime;
    private float _createMinTime;
    private float _enemyTimer;

    private bool _canSpawn = true;

    public Stack<GameObject> _spawnStack = new Stack<GameObject>();

    private void Start()
    {
        _enemyTimer = Random.Range(_createMaxTime, _createMinTime);
    }

    private void Update()
    {
        _enemyTimer += Time.deltaTime;

        if (_enemyTimer > 1/_enemySpawnFactor && (EnemyCounter.Instance.GetLimit() > EnemyCounter.Instance.GetScore())&&_canSpawn)
        {
            _enemyTimer = 0;

            int _spawnPosition = Random.Range(0, 8);

            GameObject _enemyCreate = EnemyStack();

            _enemyCreate.SetActive(true);

            _enemyCreate.transform.parent = gameObject.transform;

            _enemyCreate.transform.position = _spawnPoint[_spawnPosition].transform.position;

            EnemyCounter.Instance.EnemyIncrease(EnemyCounter.Instance.GetScore()+1);
            
        }
    }

    private GameObject EnemyStack()
    {
        if (_spawnStack.Count > 0) 
        { 
            return _spawnStack.Pop();
        }
        else
        {
            return Instantiate(_enemyPrefab, gameObject.transform);
        }
    }

    public void StopSpawn()
    {
        _canSpawn = false;
    }
    public void ResumeSpawn()
    {
        _canSpawn = true;
    }

    public void BossSpawn()
    {
        int _spawnPosition = Random.Range(0, 8);

        Instantiate(_Boss, null, _spawnPoint[_spawnPosition]);
    }
}
