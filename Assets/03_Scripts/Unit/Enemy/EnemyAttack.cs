using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    private bool _canDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (_canDamage)
            {
                StartCoroutine(AttackDelay());
                HealthManager _playerHM = collision.GetComponentInParent<HealthManager>();
                _playerHM.GetDamage(_damage);
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
