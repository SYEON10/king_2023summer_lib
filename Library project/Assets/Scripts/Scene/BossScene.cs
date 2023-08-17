using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossScene : BaseScene
{
    private static BossScene _instance;
    static BossScene GetBoss { get { Singleton(); return _instance; } }
    
    public static bool BossAlive { private get; set; } = true;

    private BossPlayerController _player;
    private BossController _boss;
    private EnemyAIController _enemy;


    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Boss;
        
        GameManager.Input.KeyAction -= OnUpdate;
        GameManager.Input.KeyAction += OnUpdate;

        GameManager.EnemyCount = 0;
        
        _player = GameManager.Resources.Instantiate("Characters/Player").GetOrAddComponent<BossPlayerController>();
        _boss = GameManager.Resources.Instantiate("Characters/Kazuha").GetOrAddComponent<BossController>();
        //추후 한 스크립트로 제어할 수 있게 스크립트 수정하기
        _enemy = gameObject.GetOrAddComponent<EnemyAIController>();
        
        _enemy.Init();
    }

    public void OnUpdate()
    {
        _enemy.OnUpdate();
    }

    public void GameOver()
    {
        /* 3인칭 카메라 사용 시
        GameObject _player = GameObject.Find("Player");
        GameManager.Resources.Destroy(_player);
        */
        GameManager.UI.ShowPopupUI<UI_GameOver>();
        StartCoroutine(GameManager.Scene.DelayLoadingScene(3.0f));
    }

    public override void Clear()
    {
        
    }

    static void Singleton()
    {
        if (_instance == null)
        {
            GameObject _obj = GameObject.Find("@Scene");
            if (_obj == null)
            {
                _obj = new GameObject("@Scene");
                _obj.AddComponent<BossScene>();
            }
            _instance = _obj.GetComponent<BossScene>();
        }
    }
}
