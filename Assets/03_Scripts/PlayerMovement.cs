using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _turnigOverRoad;
    [SerializeField]
    private float _turningSpeed;
    [SerializeField]
    private float _overRoadScale;

    bool _isDash;

    float _directionY;
    float _directionX;

    public float _acceleration;
    public float _airResistance;
    public float _speed;
    public float _maxSpeed;

    InputManager _input;
    SceneManager _scene;

    private void Awake()
    {
        _scene = GetComponent<SceneManager>();
        _input = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    // Update is called once per frame


    void Update()
    {
        //���콺�� ȸ��
        #region

        //y�� x�� �� ��ũź��Ʈ�� ��ȯ => ���� ���� ��ȯ�ϹǷ� ���������� ��ȯ



        //transform.rotation = Quaternion.AngleAxis(rotateDegree, Vector3.forward); ���� ���ư�. �� ���� �ڵ�. �ؿ��� ����.
        float _rotateDeg = _input.GetMouseDeg(transform.position);


        Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDeg - 90); //���Ϸ� ���� �޴´�.(�߿�)
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _turningSpeed * _turnigOverRoad);
        //���� �������� ��ǥ �������� ȸ���ӵ���ŭ�� �ӵ��� ȸ���Ѵ�.
        #endregion

        //����Ʋ ������ �����̱�
        #region
        if (_input.Acceleration() && _speed < _maxSpeed)
        {
            _speed += ((_acceleration - _airResistance) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) && _speed >= _maxSpeed)
        {
            _speed = _maxSpeed;
        }
        else if (_speed > 0)
        {
            _speed -= _airResistance*Time.deltaTime;
        }
        //����Ʋ ��
        float z = transform.rotation.eulerAngles.z+90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
        #endregion

        

        if (_input.BrakeButton()&& !_isDash)
        {
            _airResistance = 2;
            _turnigOverRoad = _overRoadScale;
        }
        else if(! _isDash)
        {
            _turnigOverRoad = 1;
            _airResistance = 0.5f;
        }

        if (_input.AfterBurner())
        {
            _acceleration = 4;
            _maxSpeed = 12;
        }
        else
        {
            _acceleration = 2f;
            if(_maxSpeed > 6)
            { 
                _maxSpeed = _speed;
                _maxSpeed -= _speed * Time.deltaTime * 0.1f;
            }
            else
            {
                _maxSpeed = 6;
            }
        }

        

        if (_input.StartDash())
        {
            DoDash();
        }
        
        if (_input.EndDash())
        {
            ResetDash();
        }

        
    }
    private void DoDash()
    {
        _scene.SetTime(0.2f);
        _turnigOverRoad = _overRoadScale * 2.5f;
        _isDash = true;
    }

    private void ResetDash()
    {
        _turnigOverRoad = 1f;
        _scene.SetTime(1f);
        _isDash = false;
    }
}
