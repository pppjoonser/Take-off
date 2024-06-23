using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] 
    private float _firedelay;

    [SerializeField]
    GameObject _misilePrefab;

    private ScriptControler _controler;

    public Stack<GameObject> _misilePool = new Stack<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        _controler = FindAnyObjectByType<ScriptControler>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Start()
    {
        for(int i= 0; i < _fireamount; i++)
        {
            GameObject _misileTemp = Instantiate(_misilePrefab);
            _misileTemp.SetActive(false);

            _misilePool.Push(_misileTemp);
        }
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

    private GameObject GetPool()
    {
        if(_misilePool.Count > 0)
        {
            return _misilePool.Pop();
        }
        else
        {
            return Instantiate(_misilePrefab);
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
        for (int i = 0; i < _fireamount/2; i++)
        {
            GameObject _missleTemp = GetPool();
            _missleTemp.SetActive(true);
            _missleTemp.transform.position = transform.position;
            _missleTemp.transform.rotation = Quaternion.Euler(0,0,transform.rotation.z+90);
            yield return new WaitForSeconds(_firedelay);
        }
        for (int i = 0; i < _fireamount/2; i++)
        {
            GameObject _missleTemp = GetPool();
            _missleTemp.SetActive(true);
            _missleTemp.transform.position = transform.position;
            _missleTemp.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z - 90);
            yield return new WaitForSeconds(_firedelay);
        }

        yield return new WaitForSeconds(_attackDelay);
    }
    private IEnumerator DashFoward()
    {
        _speedTemp = _speed;
        _speed = _dashSpeed;

        yield return new WaitForSeconds(_dashTime);

        _speed = _speedTemp;

    }
    public void Death()
    {
        _controler.ShowTextBox();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Destroyed();
    }
}
