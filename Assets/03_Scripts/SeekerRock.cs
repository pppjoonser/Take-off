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
        Vector3 mousePos = Input.mousePosition;//���콺 ��ġ
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mousePos);//���콺 ��ġ�� ���� ��ġ�� ��ȯ
        Vector3 objectPosition = transform.position;//�ڽ��� ��ġ �ޱ�

        _directionY = mPos.y - objectPosition.y; //y���� ��ġ���� �ޱ�
        _directionX = mPos.x - objectPosition.x; //x���� ��ġ���� �ޱ�

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg - 90;
        //y�� x�� �� ��ũź��Ʈ�� ��ȯ => ���� ���� ��ȯ�ϹǷ� ���������� ��ȯ

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
