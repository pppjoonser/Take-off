using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void TutorialStart()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetTime(float timeset)
    {
        Time.timeScale = timeset;
        Time.fixedDeltaTime = timeset * 0.02f;
    }
}