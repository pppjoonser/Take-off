using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject playerfire;
    public float _fireSpeed;
    [SerializeField] private float _delayFuze;
    Vector2 _playerspeed;
    private void Awake()
    {
        playerfire = GameObject.Find("Player");

        
    }

    private void Start()
    {
        _playerspeed = playerfire.GetComponent<Rigidbody2D>().velocity;
    }
    void Update()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2((Mathf.Cos(z * Mathf.Deg2Rad)), (Mathf.Sin(z * Mathf.Deg2Rad)));
        GetComponent<Rigidbody2D>().velocity = direction * _fireSpeed + _playerspeed;

        _delayFuze -= Time.deltaTime;
        if (_delayFuze < 0)
        {
            Destroy(gameObject);
        }
    }


}
