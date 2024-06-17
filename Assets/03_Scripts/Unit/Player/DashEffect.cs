using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class DashEffect : MonoBehaviour
{
    [SerializeField]
    GameObject _effect1, _effect2;

    [SerializeField]
    private PlayerMovement _movement;

    private Coroutine _effect;

    public void StartCharge()
    {
        _effect1.SetActive(true);
        _effect = StartCoroutine(EffectDelay());
    }
    private IEnumerator EffectDelay()
    {
        yield return new WaitForSecondsRealtime(_movement._chargingTime);
        _effect2.SetActive(true);
    }

    public void ResetEffect()
    {
        _effect1.SetActive(false);
        _effect2.SetActive(false);
        if (_effect != null)
        {
            StopCoroutine(_effect);
        }
    }
}
