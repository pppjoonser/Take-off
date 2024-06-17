using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    public bool _canDamage= true;
    HealthManager _healthManager;
    

    private void Awake()
    {
        _healthManager = GetComponentInParent<HealthManager>();
    }

    private void OnEnable()
    {
        _canDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_canDamage)
            {
                StartCoroutine(AttackDelay());
                HealthManager _playerHM = collision.gameObject.GetComponentInParent<HealthManager>();
                _playerHM.GetDamage(_damage);
                _healthManager.Splash();
            }
        }
    }

    private IEnumerator AttackDelay()
    {
        _canDamage = false;
        yield return new WaitForEndOfFrame();
        _canDamage = true;
    }
}
