using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

using Random = UnityEngine.Random;

public class BossScript : EnemyMovement
{
    private int _attackMode;
    [SerializeField]
    private float _attackDelay;

    private float _speedTemp;

    [SerializeField]
    private float _dashSpeed;

    [SerializeField]
    private GameObject _dashAttack;
    [SerializeField]
    private float _dashTime;

    private bool _isMoving = true;

    [SerializeField]
    private int _fireamount;

    protected override void Awake()
    {
        base.Awake();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Start()
    {
        StartCoroutine(StateChange());
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            DirChange(TargetDegree());
            MoveForward();
        }
    }

    private IEnumerator StateChange()
    {
        _attackMode = Random.Range(0, 2);

        switch (_attackMode)
        {
            case 0:
            StartCoroutine(DashFoward());
                break;
            case 1:
            StartCoroutine(Fire());
            break;
        }

        yield return new WaitForSeconds(_attackDelay);

        StartCoroutine(StateChange());

    }


    private IEnumerator Fire()
    {
        yield return null;
        for(int i  = 0; i < _fireamount; i++)
        {
            Debug.Log("fire");
            yield return null;
        }
    }

    private IEnumerator DashFoward()
    {
        Debug.Log("Dash");

        _speedTemp = _speed;
        _speed = _dashSpeed;

        yield return new WaitForSeconds(_dashTime);

        _speed = _speedTemp;

        yield return new WaitForSeconds(0.2f);
    }
    public void Death()
    {

    }
}
