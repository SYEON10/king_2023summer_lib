using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSceneController : MonoBehaviour
{
    private int _enemyCount = 0;
    private bool _bossAlive = true;
    private bool _playerAlive = true;

    private BossPlayerController _player;
    private BossController _boss;
    private EnemyAIController _enemy;

    private void Start()
    {
        _player = ;
        _boss = ;
        _enemy = gameObject.GetOrAddComponent<EnemyAIController>();
        
        
        
        _enemy.Init();
    }

    public void GameOver()
    {
        /* 3인칭 카메라 사용 시
        GameObject _player = GameObject.Find("Player");
        GameManager.Resources.Destroy(_player);
        */
        GameManager.UI.ShowPopupUI<UI_GameOver>();
    }
}
