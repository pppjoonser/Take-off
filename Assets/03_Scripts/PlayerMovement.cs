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
        //마우스로 회전
        #region
        Vector3 mousePos = Input.mousePosition;//마우스 위치
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mousePos);//마우스 위치를 월드 위치로 변환
        Vector3 objectPosition = transform.position;//자신의 위치 받기

        _directionY = mPos.y - objectPosition.y; //y방향 위치차이 받기
        _directionX = mPos.x - objectPosition.x; //x방향 위치차이 받기

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;
        //y값 x값 을 아크탄젠트로 변환 => 라디안 값을 반환하므로 각도값으로 변환

        //transform.rotation = Quaternion.AngleAxis(rotateDegree, Vector3.forward); 휙휙 돌아감. 맛 없는 코드. 밑에거 쓰자.

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDegree - 90); //오일러 각을 받는다.(중요)
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _turningSpeed * _turnigOverRoad);
        //현재 각도에서 목표 각도까지 회전속도만큼의 속도로 회전한다.
        #endregion

        //스로틀 값으로 움직이기
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
        //스로틀 값
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
