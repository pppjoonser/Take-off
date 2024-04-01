using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject FirePos;
    private float _fireLate;
    public float _fireReady;

    void Update()
    {
        //click the fire butten
        if (Input.GetButton("Fire1"))
        {
            if (_fireLate <= 0)
            {
                //create bullet
                GameObject bullet = Instantiate(bulletPrefab);
                //FirePos
                bullet.transform.position = FirePos.transform.position;
                bullet.transform.rotation = gameObject.transform.rotation;


                _fireLate = _fireReady;
            }
            _fireLate -= Time.deltaTime;
        }
    }
}
