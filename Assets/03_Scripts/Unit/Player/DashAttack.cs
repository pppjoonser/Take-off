using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    private bool _canDamage = true;
    [SerializeField]
    private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (_canDamage)
            {
                HealthManager _enemyHM = collision.GetComponentInParent<HealthManager>();
                _enemyHM?.GetDamage(_damage);
                StartCoroutine(AttackDelay());
            }
        }
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.02f);
    }
}
