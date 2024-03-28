using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _turningSpeed;
    public float _turnigOverRoad;
    public float _overRoadScale;


    public float _acceleration;
    public float _airResistance;
    private float _speed;
    public float _maxSpeed;

    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���콺�� ȸ��
        #region
        Vector3 mousePos = Input.mousePosition;//���콺 ��ġ
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���콺 ��ġ�� ���� ��ġ�� ��ȯ
        Vector3 objectPosition = transform.position;//�ڽ��� ��ġ �ޱ�

        float directionY = mPos.y - objectPosition.y; //y���� ��ġ���� �ޱ�
        float directionX = mPos.x - objectPosition.x; //x���� ��ġ���� �ޱ�


        float _rotateDegree = Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;
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
        else
        {
            _speed -= _airResistance*Time.deltaTime;
        }
        Debug.Log(_speed);
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
    }
}
