using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacking : EnemyAttack
{
    private Collider2D _collider;
    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider2D>();
    }
    protected override IEnumerator AttackDelay()
    {
        base.AttackDelay();
        _collider.enabled = false;
        yield return new WaitForSeconds(1);
        _collider.enabled = true;
    }
}
