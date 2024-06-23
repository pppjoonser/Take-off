using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPrint : MonoBehaviour
{
    [SerializeField] private AudioClip _typing;
    private AudioSource _sourse;

    [SerializeField]
    private string[] _scripts;
    [SerializeField]
    private string[] _names;

    [SerializeField]
    private TMP_Text _nameTag;
    [SerializeField]
    private TMP_Text _scriptPanal;

    private string _currentScript;

    private InputManager _inputManager;

    private int _index = 0;

    Coroutine _printer;

    private ScriptControler _controler;

    private void Awake()
    {
        _inputManager = FindAnyObjectByType<InputManager>();
        _controler = GetComponentInParent<ScriptControler>();
        _sourse = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _inputManager._onEnter += PrintNextText;
        PrintNextText();
    }

    private void OnDisable()
    {
        _inputManager._onEnter -= PrintNextText;
    }

    private void Start()
    {
        _sourse.clip = _typing;
        for (int i = 0; i < _scripts.Length; i++)
        {
            if (_scripts[i].Contains("  "))
            {
                _scripts[i] = _scripts[i].Replace("  ", "\n");
            }
        }
    }
    private IEnumerator Print()
    {
        
        _currentScript = _scripts[_index];
        
        _scriptPanal.text = null;

        for(int i = 0; i < _scripts[_index].Length; i++)
        {
            _scriptPanal.text += _currentScript[i];
            _sourse.Play();
            yield return new WaitForSecondsRealtime(0.05f);
        }

        _printer = null;

        _index++;
    }
    private void PrintNextText()
    {
        
        if (_index < _scripts.Length)
        {
            _nameTag.text = _names[_index];
            if (_printer != null)
            {
                StopCoroutine(_printer);
                _scriptPanal.text = _scripts[_index];
                _index++;
                _printer = null;
                return;
            }
            _printer = StartCoroutine(Print());
        }
        else _controler.ToNextChapter();
    }
}
