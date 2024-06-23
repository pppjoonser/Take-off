using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelfActivate : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }


}
