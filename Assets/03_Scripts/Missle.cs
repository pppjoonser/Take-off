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
    private void Start()
    {
        _trackingTarget = GameObject.Find("Seeker").GetComponent<SeekerRock>()._target;
    }
    void Update()
    {
        if (_trackingTarget == null)
        {
            Destroy(gameObject);
        }
        Vector3 mPos = _trackingTarget.transform.position;
        Vector3 objectPosition = transform.position;

        _directionY = mPos.y - objectPosition.y; 
        _directionX = mPos.x - objectPosition.x;

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, _rotateDegree - 90); //오일러 각을 받는다.(중요)

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _turningSpeed);


        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _fireSpeed;

        
    }

}
