using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMisile : MonoBehaviour
{
    GameObject _player;

    Vector3 _playerSpeed;

    float _directionY;
    float _directionX;

    [SerializeField]
    float _speed;
    private void Awake()
    {
        _player = GameObject.Find("Player");
    }
    void Update()
    {
        _playerSpeed = _player.GetComponent<Rigidbody2D>().position;
        Vector3 objectPosition = transform.position;
        _directionY = _playerSpeed.y - objectPosition.y; //y방향 위치차이 받기
        _directionX = _playerSpeed.x - objectPosition.x; //x방향 위치차이 받기

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(_rotateDegree - 90, Vector3.forward);

        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));

        GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
