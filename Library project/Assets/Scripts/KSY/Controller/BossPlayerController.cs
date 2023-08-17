using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    private PlayerAttack player; //추후 PlayerAttack 이랑 병합하고 없애기
    
    void Start()
    {
        player = GameObject.Find("Player").GetOrAddComponent<PlayerAttack>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        { 
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        } 
        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Kazuha")
        {
            if (Math.Truncate(player.P_LeftCoolTime) > 0)
            {
                //플레이어사망
            }
            else
            {
                BossScene.DecEnemyCount();
            }
        }
    }
}
