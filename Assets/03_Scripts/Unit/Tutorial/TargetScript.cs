using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetScript : MonoBehaviour
{
    public UnityEvent _getText;

    private bool _enabled = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && _enabled)
        {
            StartCoroutine(AvoidDude());
            _getText?.Invoke();
        }
    }

    IEnumerator AvoidDude()
    {
        _enabled = false;

        yield return new WaitForSeconds(4);

        _enabled = true;
    }
}
