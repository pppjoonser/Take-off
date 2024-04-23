using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _turnigOverRoad;
    public float _turningSpeed;
    public float _overRoadScale;


    float _directionY;
    float _directionX;

    public float _acceleration;
    public float _airResistance;
    public float _speed;
    public float _maxSpeed;

    InputManager _input;

    private void Awake()
    {
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

        Debug.Log(_rotateDeg);

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

        if (Input.GetKey(KeyCode.C))
        {
            _turnigOverRoad = _overRoadScale;
        }
        else
        {
            _turnigOverRoad = 1;
        }

        if (_input.BrakeButton())
        {
            _airResistance = 2;
        }
        else
        {
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
                _maxSpeed -= _speed * Time.deltaTime * 0.07f;
            }
            else
            {
                _maxSpeed = 6;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            _turnigOverRoad = 0;
        }
        else
        {
            _turnigOverRoad = 1;
        }
    }
}
