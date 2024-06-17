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

    
    EnemyMovement _enemy;
    PlayerMovement _player;
    BossScript _boss;

    private void Awake()
    {
        _currentHealth = _maxHealth;

    }
    private void Start()
    {
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

    
    public void Splash()
    {
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
}
