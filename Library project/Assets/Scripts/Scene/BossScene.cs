using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossScene : BaseScene
{
    private static BossScene _instance;
    static BossScene GetBoss { get { Singleton(); return _instance; } }

    //씬에 보여지는 캐릭터 관리
    private static int _enemyCount = 0;
    public static void IncEnemyCount() { _enemyCount++; }
    public static void DecEnemyCount() { _enemyCount--; }
    
    public static bool BossAlive { private get; set; } = true;
    public static bool PlayerAlive { private get; set; } = true;

    private BossPlayerController _player;
    private BossController _boss;
    private EnemyAIController _enemy;


    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Boss;
        
        GameManager.Input.KeyAction -= OnUpdate;
        GameManager.Input.KeyAction += OnUpdate;
        
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
