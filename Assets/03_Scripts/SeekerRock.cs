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

    public GameObject _target;
    public float _lockOnTime;

    PolygonCollider2D _Collider;

    private bool _isFire;

    public int _missleAmount;

    public float _missleReroadTime;
    private void Awake()
    {
        _Collider = GetComponent<PolygonCollider2D>();
    }
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

        if (Input.GetKey(KeyCode.Space))
        {
            _isFire = true;
        }
        else
        {
            _isFire = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy"&&_isFire&&_missleAmount>0)
        {
            _lockOnLate += Time.deltaTime;

            _target = collision.gameObject;

            if(_lockOnLate > _lockOnTime)
            {
                _lockOnLate = 0;

                GameObject _missleLocation = Instantiate(_missle);

                _missleLocation.transform.position = transform.position;

                _isFire = false;

                _missleAmount--;

                StartCoroutine(MissleColltime());
            }
        }
    }

    IEnumerator MissleColltime()
    {
        yield return new WaitForSecondsRealtime(_missleReroadTime);
        _missleAmount++;
    }

}
