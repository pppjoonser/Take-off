using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPointMove : MonoBehaviour
{
    public PlayerMovement _playerCharictor;
    private PlayerMovement _movement;

    private void Awake()
    {
        _movement = GetComponentInParent<PlayerMovement>();
    }
    private void FixedUpdate()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 _direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        _direction = _direction.normalized;

        if (!_playerCharictor._dashFoward) { CameraPosSet(_direction, 3f);}
        else
        {
            //CameraPosSet(_direction, 3f);
        }
    }


    private void CameraPosSet(Vector3 _targetDirection, float targetSize)
    {
        transform.position = _playerCharictor.transform.localPosition +
            new Vector3(_targetDirection.x, _targetDirection.y) * _playerCharictor._speed / targetSize;
    }

}
