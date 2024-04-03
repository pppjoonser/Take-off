using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement _playerChar;
    public GameObject _cameraPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = _cameraPos.transform.position + Vector3.forward * -1;

        Camera.main.orthographicSize = _playerChar._speed/3+1;
    }
}
