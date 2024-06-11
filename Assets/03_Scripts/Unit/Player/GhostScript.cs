using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public float _ghostDelay;
    private bool _ghostCooltime=false;
    [SerializeField]
    private GameObject ghost;
    InputManager _input;
    PlayerMovement _player;
    public Stack<GameObject> GhostPool = new Stack<GameObject>();

    private void Awake()
    {
        _input = GameObject.Find("InputManager").GetComponent<InputManager>();
        _player = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_player._dashFoward&&!_ghostCooltime)
        {
            GameObject currentGhost = GetGhost();
            currentGhost.SetActive(true);
            currentGhost.transform.position = transform.position;
            currentGhost.transform.rotation = transform.rotation;
            StartCoroutine(GhostCoolTime());
        }
    }

    private GameObject GetGhost()
    {
        if (GhostPool.Count > 0)
        {
            return GhostPool.Pop();
        }
        else
        {
            return Instantiate(ghost);
        }
    }


    
    IEnumerator GhostCoolTime()
    {
        _ghostCooltime = true;
        yield return new WaitForSeconds(_ghostDelay);
        _ghostCooltime = false;
    }
}
