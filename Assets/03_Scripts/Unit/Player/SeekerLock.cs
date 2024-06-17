using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerLock : MonoBehaviour
{
    public float _lockOnRange;
    [SerializeField] GameObject _missle;

    float _directionY;
    float _directionX;

    float _lockOnLate;

    public GameObject _target;
    public float _lockOnTime;

    PolygonCollider2D _Collider;

    [SerializeField]
    MissleUI _missleUI;

    private bool _isFire;

    public int _missleAmount;

    public float _missleReroadTime;

    public Stack<GameObject> _missilePool = new Stack<GameObject>();
    private void Awake()
    {
        _Collider = GetComponent<PolygonCollider2D>();
    }
    private void Start()
    {
        for (int i = 0; i < _missleAmount; i++)
        {
            GameObject _misiletemp = Instantiate<GameObject>(_missle);
            _missilePool.Push(_misiletemp);
            _misiletemp.SetActive(false);
        }
    }
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
        if(collision.CompareTag("Enemy")&&_isFire&&_missleAmount>0)
        {
            _lockOnLate += Time.deltaTime;

            _target = collision.gameObject;

            if(_lockOnLate > _lockOnTime)
            {
                _lockOnLate = 0;

                GameObject _missleLocation = GetPool();
                _missleLocation.SetActive(true);

                _missleLocation.transform.position = transform.position;
                _missleLocation.transform.rotation = transform.rotation;

                _isFire = false;

                _missleAmount--;

                _missleUI.Initialized(_missleAmount);

                StartCoroutine(MissleColltime());
            }
        }
    }

    private GameObject GetPool()
    {
        if (_missilePool.Count > 0)
        {
            return _missilePool.Pop();
        }
        else
        {
            return Instantiate(_missle);
        }
    }

    IEnumerator MissleColltime()
    {
        yield return new WaitForSecondsRealtime(_missleReroadTime);
        _missleAmount++;
        _missleUI.Initialized(_missleAmount);

    }

}
