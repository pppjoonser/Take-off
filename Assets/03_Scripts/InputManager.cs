using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //선언부
    #region
    public event Action _OnMouseMove;
    public event Action _CantMouseMove;

    public event Action _onSpace;
    public event Action _onAccelButton;
    public event Action _onBrake;
    public event Action _offBrake;
    public event Action _onAfterBurn;
    public event Action _offAfterBurn;
    public event Action _onFire;
    public event Action _onDashButton;
    public event Action _offDashButton;
    public event Action _engineIdle;

    float _directionY;
    float _directionX;
    #endregion

    //함수부
    #region
    public void SpaceButton()
    {
        if (Input.GetKey(KeyCode.Space))
            _onBrake?.Invoke();
    }

    public void Acceleration()
    {
        if (Input.GetKey(KeyCode.W))
            _onAccelButton?.Invoke();
        else _engineIdle?.Invoke();
    }

    public void BrakeButton()
    {
        if (Input.GetKey(KeyCode.S))
            _onBrake?.Invoke();
        else _offBrake?.Invoke();
    }

    public void AfterBurner()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _onAfterBurn?.Invoke();
        else _offAfterBurn?.Invoke();
    }

    public void Fire()
    {
        if (Input.GetButton("Fire1"))
            _onFire?.Invoke();
    }

    public void StartDash()
    {
        if (Input.GetKeyDown(KeyCode.C))
            _onDashButton?.Invoke();
    }
    
    public void EndDash()
    {
        if (Input.GetKeyUp(KeyCode.C))
            _offDashButton?.Invoke();
    }

    private void Mouseinput()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _OnMouseMove?.Invoke();
        }
        else
        {
            _CantMouseMove?.Invoke();
        }
    }
    #endregion
    public float GetMouseDeg(Vector3 objectPosition)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mousePos);

        _directionY = mPos.y - objectPosition.y;
        _directionX = mPos.x - objectPosition.x;

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;

        return _rotateDegree;
    }
    private void Update()
    {
        Mouseinput();
        SpaceButton();
        Acceleration();
        BrakeButton();
        AfterBurner();
        Fire();
        StartDash();
        EndDash();
    }
}
