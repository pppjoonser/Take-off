using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyMovement : MonoBehaviour
{
    GameObject playerfire;

    Vector3 _playerSpeed;

    EnemyCounter _enemyCounter;

    protected float _directionY;
    protected float _directionX;
    [SerializeField] protected float _enemyTurningSpeed;
    [SerializeField] protected float _speed;
    ParticleSystem particle;

    protected CircleCollider2D _collider;
    EnemyAttack _attack;
    private bool _protected = true;

    protected virtual void Awake()
    {
        playerfire = GameObject.Find("Player");
        _enemyCounter = FindObjectOfType<EnemyCounter>();
        particle = GetComponentInChildren<ParticleSystem>();
        _collider = GetComponentInChildren<CircleCollider2D>();
        _attack = GetComponentInChildren<EnemyAttack>();
    }
    
    protected virtual void OnEnable()
    {
        _protected = true;
        _collider.enabled = true;
    }
    void FixedUpdate()
    {
        DirChange(TargetDegree());
        MoveForward();
    }
    protected virtual void DirChange(float _rotateDegree)
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDegree - 90);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _enemyTurningSpeed);
    }
    protected virtual float TargetDegree()
    {
        _playerSpeed = playerfire.GetComponentInParent<Rigidbody2D>().position;
        Vector3 objectPosition = transform.position;
        _directionY = _playerSpeed.y - objectPosition.y; //y방향 위치차이 받기
        _directionX = _playerSpeed.x - objectPosition.x; //x방향 위치차이 받기

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;
        return _rotateDegree;
    }
    protected virtual void MoveForward()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }

    public void Destroyed()
    {
        if (_protected)
        {
            _attack._canDamage = false;
            EnemyCounter.Instance?.EnemyDestroy();
            particle?.Play();
            _protected = false;
            _collider.enabled = false;
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);

    }
}
