using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody rb;
    float power = 10f;
    float distanceThreshold = 1f; // �÷��̾��� �� ���� ���� �����Ÿ�
    bool canAttack = true;
    float CoolTime = 5f; // ����� ������ �������� �ð� 
    float LeftCoolTime = 0f; // ��Ÿ�� ��������� ���� �ð�

    public float P_LeftCoolTime{ get { return LeftCoolTime; } }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // �̵� 
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
        // ���� �� ��Ÿ�ӵ��� ����� �Ұ��� 
        else
        {
            if (LeftCoolTime > 0f) // ��Ÿ�� �̳� 
            {
                LeftCoolTime -= Time.deltaTime;
                Debug.Log("Cooldown: " + Mathf.Round(LeftCoolTime) + " seconds left.");
            }
            else // ��Ÿ�� ���� 
            {
                canAttack = true;
                LeftCoolTime = 0f;
            }
        }
    }

    void Attack() // �Ÿ��� �Ӱ� �Ÿ� ���ϸ� ���� 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= distanceThreshold)
            {
                enemy.SetActive(false); // �� ���� 
                LeftCoolTime = CoolTime;
                canAttack = false;
                StartCoroutine(AttackCoolTime());
                break; // �ѹ��� �� ���� ���� ���� 
            }
        }
    }

    IEnumerator AttackCoolTime()
    { // ��Ÿ�� ���� ��ٸ��� ����� �����ϵ��� 
        yield return new WaitForSeconds(CoolTime);
        canAttack = true;
    }
}