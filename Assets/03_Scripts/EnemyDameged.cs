using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDameged : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(transform.parent.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
