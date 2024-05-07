using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    bool _space;
    bool _accelButton;
    bool _brake;
    bool _afterBurn;
    bool _fire;
    bool _dashButton;
    bool _offDashButton;


    float _directionY;
    float _directionX;
    

    public float GetMouseDeg(Vector3 objectPosition)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mousePos);

        _directionY = mPos.y - objectPosition.y;
        _directionX = mPos.x - objectPosition.x;

        float _rotateDegree = Mathf.Atan2(_directionY, _directionX) * Mathf.Rad2Deg;

        return _rotateDegree;
    }

    public bool SpaceButton()
    {
        _space = Input.GetKey(KeyCode.Space);
        return _space;
    }

    public bool Acceleration()
    {
        _accelButton = Input.GetKey(KeyCode.W);
        return _accelButton;
    }

    public bool BrakeButton()
    {
        _brake = Input.GetKey(KeyCode.S);
        return _brake;
    }

    public bool AfterBurner()
    {
        _afterBurn = Input.GetKey(KeyCode.LeftShift);
        return _afterBurn;
    }

    public bool Fire()
    {
        _fire = Input.GetButton("Fire1");
        return _fire;
    }

    public bool StartDash()
    {
        _dashButton = Input.GetKeyDown(KeyCode.C);
        return _dashButton;
    }
    
    public bool EndDash()
    {
        _offDashButton = Input.GetKeyUp(KeyCode.C);
        return _offDashButton;
    }
}
