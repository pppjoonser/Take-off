using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject playerfire;
    public float _fireSpeed;
    [SerializeField] private float _delayFuze;
    Vector2 _playerspeed;
    [SerializeField]
    private float _damage;

    Playerfire gun;

    private bool _canDamage = true;
    private void Awake()
    {
        playerfire = GameObject.Find("Player");

        gun = FindAnyObjectByType<Playerfire>();

    }
    private void OnEnable()
    {
        StartCoroutine(DelayFuze());
        _playerspeed = gun.GetComponent<Rigidbody2D>().velocity;
        _canDamage = true;
    }
    private void Start()
    {
        
    }
    void FixedUpdate()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _fireSpeed + _playerspeed;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (_canDamage)
            {
                StartCoroutine(DamageDelay());
                HealthManager _enemyHM = collision.GetComponentInParent<HealthManager>();
                _enemyHM?.GetDamage(_damage);
                Disable();
            }
        }
    }
    IEnumerator DelayFuze()
    {
        yield return new WaitForSeconds(_delayFuze);
        Disable();
    }

    private void Disable()
    {
        gun.bulletPool.Push(gameObject);
        gameObject.SetActive(false);
    }
    private IEnumerator DamageDelay()
    {
        _canDamage = false;
        yield return new WaitForEndOfFrame();
        _canDamage = true;
    }
}
