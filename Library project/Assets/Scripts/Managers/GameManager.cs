using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    static GameManager GetGM
    {
        get
        {
            Init();
            return _instance;
        }
    }

    private InputManager _input = new InputManager();

    public static InputManager Input
    {
        get
        {
            return GetGM._input;
        }
    }

    private ResourceManager _resource = new ResourceManager();

    public static ResourceManager Resource
    {
        get
        {
            return _instance._resource;
        }
    }
    
    private UIManager _ui = new UIManager();
    public static UIManager UI
    {
        get
        {
            return _instance._ui;
        }
    }

    private DataManager _data = new DataManager();

    public static DataManager Data
    {
        get
        {
            return _instance._data;
        }
    }

    void Start()
    {
        Init();
        _instance._data.Init();
    }

    
    void Update()
    {
        Input.OnUpdate();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject _obj = GameObject.Find("@GameManager");
            if (_obj == null)
            {
                _obj = new GameObject("@GameManager");
                _obj.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(_obj);
            _instance = _obj.GetComponent<GameManager>();
        }
    }
}
