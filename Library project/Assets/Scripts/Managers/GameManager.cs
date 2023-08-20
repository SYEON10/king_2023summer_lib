using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static int EnemyCount = 0;
    public static bool PlayerAlive { private get; set; } = true;
    public static bool BossAlive { private get; set; } = true;

    
    #region Managers
    private static GameManager _instance;
    static GameManager GetGm
    {
        get
        {
            Init();
            return _instance;
        }
    }

    private InputManager _input = new InputManager();
    private ResourceManager _resource = new ResourceManager();
    private UIManager _ui = new UIManager();
    private DataManager _data = new DataManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private PoolManager _pool = new PoolManager();
    private SoundManager _sound = new SoundManager();


    public static InputManager Input { get { Init();return _instance._input; } }
    public static ResourceManager Resources { get { return _instance._resource; } }
    public static UIManager UI { get { Init();return _instance._ui; } }
    public static DataManager Data { get { return _instance._data; } }
    public static SceneManagerEx Scene { get { return _instance._scene; } }
    public static PoolManager Pool  { get { return _instance._pool; } }
    public static SoundManager Sound  { get { return _instance._sound; } }

    
    #endregion

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
            
            //기타 초기화
            _instance._pool.Init();
            _instance._sound.Init();
        }
    }

    public static void GameOver()
    {
        /* 3인칭 카메라 사용 시 -> Player 사망
        GameObject _player = GameObject.Find("Player");
        GameManager.Resources.Destroy(_player);
        */
        if (PlayerAlive == false || BossAlive == false)
            return;

        UI.ShowPopupUI<UI_GameOver>();
    }
    
}
