using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject playerfire;

    Vector3 _playerSpeed;

    float _directionY;
    float _directionX;
    [SerializeField] float _enemyTurningSpeed;
    [SerializeField] float _speed;
    private void Awake()
    {
        playerfire = GameObject.Find("Player");
    }
    void Update()
    {
        DirChange(TargetDegree());
        MoveForward();
    }
    private void DirChange(float _rotateDegree)
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDegree - 90);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _enemyTurningSpeed);
    }
    private float TargetDegree()
    {
        _playerSpeed = playerfire.GetComponent<Rigidbody2D>().position;
        Vector3 objectPosition = transform.position;
        _directionY = _playerSpeed.y - objectPosition.y; //y방향 위치차이 받기
        _directionX = _playerSpeed.x - objectPosition.x; //x방향 위치차이 받기

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;
        return _rotateDegree;
    }
    private void MoveForward()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }
}
