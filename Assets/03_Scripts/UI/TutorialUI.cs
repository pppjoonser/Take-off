using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    private InputManager _inputManager;

    [SerializeField]
    private GameObject[] _TutorialUI;

    private int _TutorialUICount=0;

    private void Awake()
    {
        _inputManager = FindAnyObjectByType<InputManager>();
    }

    private void OnEnable()
    {
        _inputManager._onEnter += ToNextTutorial;
    }

    public void ToNextTutorial()
    {
        StartCoroutine(NextTutorial());
    }

    private void OnDisable()
    {
        _inputManager._onEnter -= ToNextTutorial;
    }
    IEnumerator NextTutorial()
    {
        _TutorialUI[_TutorialUICount].SetActive(false);
        _TutorialUICount++;

        yield return new WaitForSeconds(0.2f);
        _TutorialUI[_TutorialUICount ]?.SetActive(true);
    }
}
