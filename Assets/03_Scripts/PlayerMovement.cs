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

    InputManager _input = InputManager.Instance;

    // Update is called once per frame
    void Update()
    {
        //���콺�� ȸ��
        #region
        Vector3 mousePos = Input.mousePosition;//���콺 ��ġ
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mousePos);//���콺 ��ġ�� ���� ��ġ�� ��ȯ
        Vector3 objectPosition = transform.position;//�ڽ��� ��ġ �ޱ�

        _directionY = mPos.y - objectPosition.y; //y���� ��ġ���� �ޱ�
        _directionX = mPos.x - objectPosition.x; //x���� ��ġ���� �ޱ�

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;
        //y�� x�� �� ��ũź��Ʈ�� ��ȯ => ���� ���� ��ȯ�ϹǷ� ���������� ��ȯ

        //transform.rotation = Quaternion.AngleAxis(rotateDegree, Vector3.forward); ���� ���ư�. �� ���� �ڵ�. �ؿ��� ����.

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDegree - 90); //���Ϸ� ���� �޴´�.(�߿�)
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _turningSpeed * _turnigOverRoad);
        //���� �������� ��ǥ �������� ȸ���ӵ���ŭ�� �ӵ��� ȸ���Ѵ�.
        #endregion

        //����Ʋ ������ �����̱�
        #region
        if (Input.GetKey(KeyCode.W) && _speed < _maxSpeed)
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

        if (Input.GetKey(KeyCode.S))
        {
            _airResistance = 2;
        }
        else
        {
            _airResistance = 0.5f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
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
