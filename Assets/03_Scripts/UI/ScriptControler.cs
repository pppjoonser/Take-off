using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptControler : MonoBehaviour
{
    [SerializeField]
    private SceneManagerScript _scene;

    [SerializeField]
    private GameObject[] _scriptUI;

    private int _index = 0;

    InputManager _input;

    private void Awake()
    {
        _input = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        _scene.SetTime(0);
    }

    public void ToNextChapter()
    {
        _input.Toggle(true);
        _scriptUI[_index].SetActive(false);
        _index++;
        _scene.SetTime(1);
        if (_index > _scriptUI.Length)
        {
            _scene.ToTitle();
        }
    }

    public void ShowTextBox()
    {

        if (_index < _scriptUI.Length)
        {
            _input.Toggle(false);
            _scriptUI[_index]?.SetActive(true);
            _scene.SetTime(0);
        }
    }

}
