using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 2;
    private float _currentHealth;
    private bool _isEnemy;

    EnemyMovement _enemy;

    private void Awake()
    {
        _currentHealth = _maxHealth;

        _isEnemy = TryGetComponent(out _enemy);
    }
    private void Start()
    {
        _isEnemy = gameObject.CompareTag("Enemy");
        if(_isEnemy)
        {
            _enemy = GetComponent<EnemyMovement>();

        }
    }
    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if( _currentHealth <= 0 ) 
        { 
            Splash(); 
        }
    }

    
    private void Splash()
    {
        if( _isEnemy)
        {
            _enemy.Destroyed();
        }
    }
}
