using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private EnemyAIController _enemy;
    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        
        _enemy = Util.GetOrCreateObject("@EnemyController").GetOrAddComponent<EnemyAIController>();
        _enemy.Init();
    }
    
}
