using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 2;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
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
        gameObject.SetActive(false);
    }
}
