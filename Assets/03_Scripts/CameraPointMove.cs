using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointMove : MonoBehaviour
{
    public PlayerMovement _playerCharictor;
    void FixedUpdate()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        direction = direction.normalized;
        transform.position = _playerCharictor.transform.localPosition +
            new Vector3(direction.x,direction.y)*_playerCharictor._speed/4;
    }
}
