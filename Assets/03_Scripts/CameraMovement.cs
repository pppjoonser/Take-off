using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public PlayerMovement _playerCharictor;
    public GameObject _cameraPos;

    private bool _canmoveCamara = true;
    // Update is called once per frame

    void Update()
    {
        if (_canmoveCamara)
        {
            Camera.main.orthographicSize = _playerCharictor._speed / 2 + 1;
        }
        transform.position = _cameraPos.transform.position + Vector3.forward * -1;

        
    }

    public void CameraSet()
    {
        StartCoroutine(CameraRock());
    }
    private IEnumerator CameraRock()
    {
        _canmoveCamara = false;
        yield return new WaitForSeconds(_playerCharictor._dashTime);
        _canmoveCamara = true;
    }
}
