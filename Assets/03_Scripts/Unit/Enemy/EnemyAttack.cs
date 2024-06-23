using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private bool _selfDestroy;
    public bool _canDamage= true;
    HealthManager _healthManager;
    

    protected virtual void Awake()
    {
        _healthManager = GetComponentInParent<HealthManager>();
    }

    private void OnEnable()
    {
        _canDamage = true;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            if (_canDamage)
            {
                StartCoroutine(AttackDelay());
                HealthManager _playerHM = collision.gameObject.GetComponentInParent<HealthManager>();
                _playerHM.GetDamage(_damage);
                if (_selfDestroy)
                {
                    _healthManager.Splash();
                }
            }
        }
    }

    protected virtual IEnumerator AttackDelay()
    {
        _canDamage = false;
        yield return new WaitForSecondsRealtime(0.2f);
        _canDamage = true;
    }
}
