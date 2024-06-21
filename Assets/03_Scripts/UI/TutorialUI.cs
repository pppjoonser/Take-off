using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    private Image _panal;
    private InputManager _inputManager;

    [SerializeField]
    private SceneManagerScript _scene;

    [SerializeField]
    private GameObject[] _TutorialUI;

    private int _TutorialUICount=0;

    private void Awake()
    {
        _inputManager = FindAnyObjectByType<InputManager>();
        _panal =GetComponent<Image>();
    }

    private void OnEnable()
    {
        _inputManager._onEnter += ToNextTutorial;
    }

    public void ToNextTutorial()
    {
        if (_TutorialUICount < _TutorialUI.Length) StartCoroutine(NextTutorial());
        else _scene.ToTitle();
    }

    private void OnDisable()
    {
        _inputManager._onEnter -= ToNextTutorial;
    }
    IEnumerator NextTutorial()
    {
        _TutorialUI[_TutorialUICount].SetActive(false);
        _TutorialUICount++;

        while (_panal.color.a >= 0)
        {
            _panal.color -= new Color(0,0,0,0.01f);
            yield return null;
        }


        yield return new WaitForSeconds(0.2f);
        if(_TutorialUICount < _TutorialUI.Length) _TutorialUI[_TutorialUICount ]?.SetActive(true);
        else _scene.ToTitle();

        while (_panal.color.a <= 1)
        {
            _panal.color += new Color(0, 0, 0, 0.01f);
            yield return null;
        }
    }
}
