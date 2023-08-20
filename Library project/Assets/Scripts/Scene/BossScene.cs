using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossScene : BaseScene
{
    private BossPlayerController _player;
    private BossController _boss;
    private EnemyAIController _enemy;
    
    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        
        GameManager.Sound.Play("BossBGM", Define.Sound.Bgm);

        SceneType = Define.Scene.Boss;
        
        GameManager.Input.KeyAction -= OnUpdate;
        GameManager.Input.KeyAction += OnUpdate;

        GameManager.EnemyCount = 0;
        
        _player = GameObject.FindGameObjectWithTag("Player").GetOrAddComponent<BossPlayerController>();
        if(_player == null)
            GameManager.Resources.Instantiate("Characters/Player").GetOrAddComponent<BossPlayerController>();
        _boss = GameManager.Resources.Instantiate("Characters/Kazuha").GetOrAddComponent<BossController>();

        _enemy = Util.GetOrCreateObject("@EnemyController").GetOrAddComponent<EnemyAIController>();
        
        _enemy.Init();
    }

    public void OnUpdate()
    {
        _enemy.OnUpdate();
    }

    public void GameClear()
    {
        /* 3인칭 카메라 사용 시
        GameObject _player = GameObject.Find("Player");
        GameManager.Resources.Destroy(_player);
        */
        GameManager.UI.ShowPopupUI<UI_GameClear>();
        StartCoroutine(GameManager.Scene.DelayLoadingScene(3.0f));
    }
}
