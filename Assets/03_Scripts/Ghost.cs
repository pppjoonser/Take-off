using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    GhostScript _ghostSpawn;
    // Start is called before the first frame update
    void Start()
    {
        _ghostSpawn = FindAnyObjectByType<GhostScript>();
    }
    private void OnEnable()
    {
        StartCoroutine(GhostDisappear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GhostDisappear()
    {
        yield return new WaitForSeconds(1f);
        _ghostSpawn.GhostPool.Push(gameObject);
        gameObject.SetActive(false);
    }

}
