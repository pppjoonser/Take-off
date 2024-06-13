using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public PlayerMovement _playerCharictor;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    private bool _canmoveCamara = true;
    private float _speedRegister;
    // Update is called once per frame

    void LateUpdate()
    {
        if (_canmoveCamara)
        {
            _speedRegister = _playerCharictor._speed;
        }

        CameraSizeSet(_speedRegister);
        
    }

    private void CameraSizeSet(float _targetSize)
    {
        virtualCamera.m_Lens.OrthographicSize = _targetSize / 3 + 1;
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
