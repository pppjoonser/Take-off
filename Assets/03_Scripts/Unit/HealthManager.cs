using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if(_unitType == 1)
        {
            _player?.Damaged(_currentHealth, _maxHealth);
        }

        if( _currentHealth <= 0 ) 
        { 
            Splash(); 
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
}
