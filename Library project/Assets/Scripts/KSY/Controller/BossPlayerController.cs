using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerController : MonoBehaviour
{
    private PlayerAttack player; //추후 PlayerAttack 이랑 병합하고 없애기
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetOrAddComponent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameManager.Sound.Play("BossEffectTest", Define.Sound.Effect);
            if (!player.canAttack)
            {
                GameManager.GameOver();
                GameManager.PlayerAlive = false;
            }
            else
            {
                GameManager.EnemyCount--;
            }
        }
    }
}
