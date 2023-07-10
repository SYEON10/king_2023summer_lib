using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump2 : MonoBehaviour
{
    bool jDown;
    bool isJumping; // ���� ������ ���θ� ��Ÿ���� ���� ���� �߰�

    Rigidbody rigid;
    Vector3 moveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();      //Rigidbody ������Ʈ�� �޾ƿ�
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // �߰��� ���ǹ�
        {
            isJumping = true; // ���� ������ ���� ����
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // �ٴڿ� ����� ��
        {
            isJumping = false; // ���� ���� ���� ����
        }
    }
}