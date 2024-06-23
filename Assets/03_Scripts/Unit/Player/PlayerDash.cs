using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField]
    private GameObject _movementCollider;
    [SerializeField]
    private GameObject _dashAttack;
    [SerializeField]
    private GameObject _camera;

    [SerializeField]
    private Playerfire _playerfire; 

    private PlayerMovement _movement;

    private CameraMovement _cameraMove;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _cameraMove = _camera.GetComponent<CameraMovement>();
    }
    public void DashAttack()
    {
        StartCoroutine(DashFoward());
    }
    private IEnumerator DashFoward()
    {
        _cameraMove?.CameraSet();
        _dashAttack.SetActive(true);
        _movementCollider.gameObject.SetActive(false);
        _movement._canRotate = false;

        float pak = _movement._speed;

        _movement._dashFoward = true;

        yield return new WaitForSeconds(_movement._dashTime);

        _movement._speed = pak;

        _movement._dashFoward = false;
        _dashAttack.SetActive(false);
        _movementCollider.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.04f);
        _playerfire.SetMainGunFireable(true);
        yield return new WaitForSeconds(0.2f);
        _movement._canRotate = true;
    }
}
