using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField]
    GameObject _missleLuncher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            GameObject _misile = Instantiate(_missleLuncher);

            _misile.transform.position = transform.position;
        }
    }
    
}
