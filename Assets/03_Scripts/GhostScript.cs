using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public float _ghostDelay;
    private bool _isGhost = true;
    private bool _ghostCooltime=false;
    [SerializeField]
    private GameObject ghost;

    public Stack<GameObject> GhostPool = new Stack<GameObject>();

    private void Awake()
    {
        InputManager _input = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGhost&&!_ghostCooltime)
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
