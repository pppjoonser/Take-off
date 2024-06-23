using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    private float _currentHealth;
    [SerializeField]
    private int _unitType;

    [SerializeField]
    bool _castCount;
    [SerializeField]
    private bool _selfDestroy;
    [SerializeField]
    private float _destroyTime;

    private bool _damageable = true;
    
    EnemyMovement _enemy;
    PlayerMovement _player;
    BossScript _boss;

    private void Start()
    {
        if (_selfDestroy)
        {
            SetHealth();
        }

        switch (_unitType)
        {
            case 0:
            _enemy = GetComponent<EnemyMovement>();
            break;
            case 1:
            _player = GetComponent<PlayerMovement>();
            break;
            case 2:
            _boss = GetComponent<BossScript>();
            break;
        }
        
    }

    private void OnEnable()
    {
        _damageable = true;
        HealthRestore();
    }
    public void GetDamage(float damage)
    {
        if (_damageable) { 
            _currentHealth -= damage;
            StartCoroutine(DamageDelay());
            if (_unitType == 1)
            {
               _player?.Damaged(_currentHealth, _maxHealth);
            }
            
            if (_currentHealth <= 0)
            {
               Splash();
            }
        }
    }

    public void HealthRestore()
    {
        _currentHealth = _maxHealth;
        if (_unitType == 1)
        {
            _player?.Damaged(_currentHealth, _maxHealth);
        }
    }
    public void Splash()
    {
        if(_castCount) {
            switch (_unitType)
            {
                case 0:
                    _enemy?.Destroyed();
                    break;

                case 1:
                    _player?.Defeat();
                    break;

                case 2:
                    _boss?.Death();
                    break;
            }
        }
        else
        {
            _enemy?.ImmidateDestroy();
        }
    }
    
    public void SetHealth()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);
        Splash();
    }

    private IEnumerator DamageDelay()
    {
        _damageable = false;
        yield return new WaitForSecondsRealtime(0.04f);
        _damageable = true;
    }
}
