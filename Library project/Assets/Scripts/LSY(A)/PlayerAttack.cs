using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody rb;
    float power = 10f;
    float distanceThreshold = 1f; // 플레이어의 적 공격 가능 범위거리
    bool canAttack = true;
    float CoolTime = 5f; // 재공격 가능할 때까지의 시간 
    float LeftCoolTime = 0f; // 쿨타임 끝나기까지 남은 시간

    public float P_LeftCoolTime{ get { return LeftCoolTime; } }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 이동 
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * power);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * power);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * power);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * power);
        }

        if (canAttack)
        {
            Attack();
        }
        // 공격 후 쿨타임동안 재공격 불가능 
        else
        {
            if (LeftCoolTime > 0f) // 쿨타임 이내 
            {
                LeftCoolTime -= Time.deltaTime;
                Debug.Log("Cooldown: " + Mathf.Round(LeftCoolTime) + " seconds left.");
            }
            else // 쿨타임 종료 
            {
                canAttack = true;
                LeftCoolTime = 0f;
            }
        }
    }

    void Attack() // 거리가 임계 거리 이하면 공격 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= distanceThreshold)
            {
                enemy.SetActive(false); // 적 죽음 
                LeftCoolTime = CoolTime;
                canAttack = false;
                StartCoroutine(AttackCoolTime());
                break; // 한번에 한 개만 공격 가능 
            }
        }
    }

    IEnumerator AttackCoolTime()
    { // 쿨타임 동안 기다리고 재공격 가능하도록 
        yield return new WaitForSeconds(CoolTime);
        canAttack = true;
    }
}