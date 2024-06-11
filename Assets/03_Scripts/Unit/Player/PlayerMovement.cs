using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //선언부
    #region
    [SerializeField]
    private float _turnigOverRoad;
    [SerializeField]
    private float _turningSpeed;
    [SerializeField]
    private float _overRoadScale;
    public float _dashTime;
    [SerializeField]
    private GameObject _camera;

    private float _dashCharge;

    [SerializeField]
    private GameObject _dashAttack;
    private CameraMovement _cameraMove;

    [SerializeField]
    private GameObject _movementCollider;

    bool _isDash;
    bool _canRotate;
    public bool _dashFoward;

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _airResistance;
    public float _speed;
    [SerializeField]
    private float _maxSpeed;

    InputManager _input;
    SceneManager _scene;


    #endregion
    private void Awake()
    {
        _scene = GetComponent<SceneManager>();
        _input = GameObject.Find("InputManager").GetComponent<InputManager>();
        _cameraMove = _camera.GetComponent<CameraMovement>();
    }

    private void Start()
    {
        _input._OnMouseMove += CanMouseMove;
        _input._CantMouseMove += CantMouseMove;
        _input._onAccelButton += SpeedSet;
        _input._onBrake += ReduceSpeed;
        _input._onDashButton += DoDash;
        _input._offDashButton += ResetDash;
        _input._offBrake += ResetSpeed;
        _input._onAfterBurn += AfterBurn;
        _input._offAfterBurn += OffAfterBurn;
        _input._engineIdle += Idle;
    }//액션 할당



    void Update()
    {
        //마우스로 회전
        #region
        if (_canRotate)
        {
            float _rotateDeg = _input.GetMouseDeg(transform.position);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDeg - 90); //오일러 각을 받는다.(중요)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _turningSpeed * _turnigOverRoad);
        }
        //현재 각도에서 목표 각도까지 회전속도만큼의 속도로 회전한다.
        #endregion

        //스로틀 값으로 움직이기
        #region
        //스로틀 값
        float z = transform.rotation.eulerAngles.z+90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
        #endregion

        if (_dashFoward)
        {
            _speed = 30;
            CantMouseMove();
            
        }
        
    }
    //함수부
    #region
    

    private void DoDash()
    {
        _scene.SetTime(0.2f);
        _turnigOverRoad = _overRoadScale * 2.5f;
        _isDash = true;
    }

    private void ResetDash()
    {
        StartCoroutine(DashFoward());
        
        _turnigOverRoad = 1f;
        _scene.SetTime(1f);
        _isDash = false;
    }
    private void CanMouseMove()
    {
        _canRotate = false;
    }
    private void CantMouseMove()
    {
        _canRotate = true;
    }
    private void SpeedSet()
    {
        if ( _speed < _maxSpeed&&!_dashFoward)
        {
            _speed += _acceleration* Time.deltaTime;
        }
        else if (_speed >= _maxSpeed && !_dashFoward)
        {
            _speed = _maxSpeed;
        }
    }
    private void ReduceSpeed()
    {
        if (!_isDash)
        {
            _airResistance = 2;
            _turnigOverRoad = _overRoadScale;
        }

    }
    private void ResetSpeed()
    {
        if (!_isDash)
        {
            _turnigOverRoad = 1;
            _airResistance = 0.5f;
        }
    }

    private void AfterBurn()
    {
        _acceleration = 4;
        _maxSpeed = 12;
    }
    private void OffAfterBurn()
    {
        _acceleration = 2f;
        if (_maxSpeed > 6)
        {
            _maxSpeed = _speed;
            _maxSpeed -= _speed * Time.deltaTime * 0.1f;
        }
        else
        {
            _maxSpeed = 6;
        }
    }
    private void Idle()
    {
        if (_speed > 0)
        {
            _speed -= _airResistance * Time.deltaTime;
        }
        else _speed = 0;
    }

    private IEnumerator DashFoward()
    {
        _cameraMove.CameraSet();
        _dashAttack.SetActive(true);
        _movementCollider.gameObject.SetActive(false);

        float pak = _speed;

        _dashFoward = true;

        yield return new WaitForSeconds(_dashTime);

        _speed = pak;

        _dashFoward = false;
        _dashAttack.SetActive(false);
        _movementCollider.gameObject.SetActive(true);
    }
    #endregion
}
