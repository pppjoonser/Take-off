using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject FirePos;
    private float _fireLate;
    public float _fireReady;
    public float _bulletFireSpeed;

    public float _bulletAcceleration;

    public delegate float BulletFire(float _bullet);

    static PlayerMovement _PC;

    public bool _readyToFire;
    private bool _isFire;

    static public float AddSpeed(float _bulletSpeed, BulletFire bullet)
    {
        return _bulletSpeed + _PC._speed;
    }



    

    void Update()
    {
        
        //click the fire butten
        if (Input.GetButton("Fire1"))
        {
            _isFire = true;
        }
        else
        {
            _isFire = false;
        }
        if (_fireLate <= 0)
        {
            _readyToFire = true;
        }
        else
        {
            _readyToFire = false;
        }
    }

    private void FixedUpdate()
    {
        if (_readyToFire && _isFire)
        {
            //create bullet
            GameObject bullet = Instantiate(bulletPrefab);
            //FirePos
            bullet.transform.position = FirePos.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;

            //BulletFire bulletFire = new BulletFire(AddSpeed);

            //bulletFire.Invoke(_bulletFireSpeed);

            _fireLate = _fireReady;
        }
        _fireLate -= 0.01f;
    }
}
