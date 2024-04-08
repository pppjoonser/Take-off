using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerRock : MonoBehaviour
{
    public float _lockOnRange;
    [SerializeField] GameObject _missle;

    float _directionY;
    float _directionX;

    float _lockOnLate;

    static GameObject _target;
    public float _lockOnTime;
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;//마우스 위치
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mousePos);//마우스 위치를 월드 위치로 변환
        Vector3 objectPosition = transform.position;//자신의 위치 받기

        _directionY = mPos.y - objectPosition.y; //y방향 위치차이 받기
        _directionX = mPos.x - objectPosition.x; //x방향 위치차이 받기

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg - 90;
        //y값 x값 을 아크탄젠트로 변환 => 라디안 값을 반환하므로 각도값으로 변환

        transform.rotation = Quaternion.AngleAxis(_rotateDegree, Vector3.forward);

        Debug.DrawRay(transform.position, transform.up * _lockOnRange, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, _lockOnRange, LayerMask.GetMask("Enemy"));
        if(hit.collider != null)
        {
            _lockOnLate += Time.deltaTime;
            if (_lockOnLate > _lockOnTime)
            {
                GameObject _misile = Instantiate(_missle);
                _missle.transform.position = transform.position;
                _lockOnLate = 0;
                _target = _misile;
            }
        }
        else
        {
            _lockOnLate = 0;
        }
    }
    
    public static GameObject GetTarGet()
    {
        return _target;
    }
}
