using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    GameObject _trackingTarget;
    private float _directionY;
    private float _directionX;
    [SerializeField]
    private float _fireSpeed;
    [SerializeField]
    private float _turningSpeed;
    private bool _canDamage = true;

    SeekerRock _seeker;

    [SerializeField]
    private float _damage;
    private void Start()
    {
        _seeker = GameObject.Find("Seeker").GetComponent<SeekerRock>();
        _trackingTarget = _seeker._target;
        StartCoroutine(DelayFuze());
    }
    void Update()
    {
        if (!_trackingTarget.gameObject.activeSelf)
        {
            Disable();
        }
        Vector3 mPos = _trackingTarget.transform.position;
        Vector3 objectPosition = transform.position;

        _directionY = mPos.y - objectPosition.y; 
        _directionX = mPos.x - objectPosition.x;

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDegree - 90); //���Ϸ� ���� �޴´�.(�߿�)

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _turningSpeed);


        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _fireSpeed;

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (_canDamage)
            {
                StartCoroutine(DamageDelay());
                HealthManager _enemyHM = collision.GetComponentInParent<HealthManager>();
                _enemyHM.GetDamage(_damage);
                Disable();
            }
        }
    }
    private void Disable()
    {
        _seeker._missilePool.Push(gameObject);
        gameObject.SetActive(false);
    }
    private IEnumerator DamageDelay()
    {
        _canDamage = false;
        yield return new WaitForEndOfFrame();
        _canDamage = true;
    }
    private IEnumerator DelayFuze()
    {
        yield return new WaitForSeconds(8);
        Disable();
    }

}
