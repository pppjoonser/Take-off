using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement _PC;
    // Update is called once per frame
    void Update()
    {
        Vector3 _target = _PC.transform.position + new Vector3(0, 0, -1);
        transform.position = _target;

        Camera.main.orthographicSize = _PC._speed/3+1;
    }
}
