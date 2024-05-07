using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    
    public void SetTime(float timeset)
    {
        Time.timeScale = timeset;
        Time.fixedDeltaTime = timeset * 0.02f;
    }
}
