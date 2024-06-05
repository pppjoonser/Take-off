using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public PlayerMovement _playerCharictor;
    public GameObject _cameraPos;

    private bool _canmoveCamara = true;
    private float _speedRegister;
    // Update is called once per frame

    void Update()
    {
        if (_canmoveCamara)
        {
            _speedRegister = _playerCharictor._speed;
        }

        
        CameraSizeSet(_speedRegister);
        
        
        transform.position = _cameraPos.transform.position + Vector3.forward * -1;

        
    }
    private void LateUpdate()
    {
        
    }

    private void CameraSizeSet(float _targetSize)
    {
        Camera.main.orthographicSize = _targetSize / 3 + 1;
    }

    public void CameraSet()
    {
        StartCoroutine(CameraRock());
    }
    private IEnumerator CameraRock()
    {
        _speedRegister = _playerCharictor._speed;
        _canmoveCamara = false;
        yield return new WaitForSeconds(_playerCharictor._dashTime);
        _canmoveCamara = true;
    }
}
