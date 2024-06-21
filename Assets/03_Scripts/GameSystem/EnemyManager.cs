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

            GameObject _enemyCreate = Instantiate(_enemyPrefab);

            _enemyCreate.transform.position = _spawnPoint[_spawnPosition].transform.position;

            EnemyCounter.Instance.EnemyIncrease(EnemyCounter.Instance.GetScore()+1);
            
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
        Instantiate(_Boss);
    }
}
