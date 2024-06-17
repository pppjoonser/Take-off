using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleUI : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab, healthPanal;
    [SerializeField]
    private int heartCount;

    private void Start()
    {
        Initialized(heartCount);
    }
    public void Initialized(int liveCount)
    {
        //DestroyHeart that already exist

        //Create new Heart Until amount of Heart

        foreach (Transform child in healthPanal.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < liveCount; i++)
        {
            Instantiate(heartPrefab, healthPanal.transform);
        }
    }
}
