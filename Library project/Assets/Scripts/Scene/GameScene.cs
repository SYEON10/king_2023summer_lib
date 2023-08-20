using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        SceneType = Define.Scene.Game;
    }
    
}
