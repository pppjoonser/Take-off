using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject FirePos;
    public float _fireRate;
    public float _bulletFireSpeed;

    public float _bulletAcceleration;

    public delegate float BulletFire(float _bullet);

    private InputManager _inputManager;
    static PlayerMovement _PC;

    private bool _readyToFire = true;

    public Stack<GameObject> bulletPool = new Stack<GameObject>();

    static public float AddSpeed(float _bulletSpeed, BulletFire bullet)
    {
        return _bulletSpeed + _PC._speed;
    }
    private void Awake()
    {
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }
    private void Start()
    {
        _inputManager._onFire += PlayerFire;
        _readyToFire = true;
    }
    void Update()
    {
        
        
        
    }

    private GameObject GetBulletInPool()
    {
        if (bulletPool.Count > 0)
        {
            return bulletPool.Pop();
        }
        else
        {
            return Instantiate(bulletPrefab);
        }
    }

    private void PlayerFire()
    {
        if (_readyToFire)
        {
            //create bullet
            GameObject bullet = GetBulletInPool();
            //FirePos
            bullet.SetActive(true);
            bullet.transform.position = FirePos.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;
            StartCoroutine(ShootCoolTime());
        }
    }

    public void SetMainGunFireable(bool setting)
    {
        if (setting)
        {
            _inputManager._onFire += PlayerFire;
        }
        else 
        {
            _inputManager._onFire -= PlayerFire;
        }
    }

    IEnumerator ShootCoolTime()
    {
        _readyToFire = false;
        yield return new WaitForSeconds(_fireRate);
        _readyToFire = true;

    }
}
