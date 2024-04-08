using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;


    float _directionY;
    float _directionX;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
