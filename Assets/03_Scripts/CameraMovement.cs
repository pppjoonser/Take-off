using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement _playerCharictor;
    public GameObject _cameraPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = _cameraPos.transform.position + Vector3.forward * -1;

        Camera.main.orthographicSize = _playerCharictor._speed/2+1;
    }
}
