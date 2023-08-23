using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossScene : BaseScene
{
    private BossController _boss;
    private EnemyAIController _enemy;
    private Coroutine _clearCoroutine;
    
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

        _boss = GameManager.Resources.Instantiate("Characters/Kazuha").GetOrAddComponent<BossController>();

        _enemy = Util.GetOrCreateObject("@EnemyController").GetOrAddComponent<EnemyAIController>();
        
        _enemy.Init();

        StartCoroutine(GameClear_Coroutine());
    }

    public void OnUpdate()
    {
        _enemy.OnUpdate();
    }

    IEnumerator GameClear_Coroutine()
    {
        yield return new WaitForSeconds(100.0f);
        GameClear();
    }

    public void GameClear()
    {
        /* 3인칭 카메라 사용 시
        GameObject _player = GameObject.Find("Player");
        GameManager.Resources.Destroy(_player);
        */
        
        //+모든 몬스터 삭제
        GameManager.UI.ShowPopupUI<UI_GameClear>();
        StartCoroutine(GameManager.Scene.DelayLoadingScene(3.0f));
    }
}
