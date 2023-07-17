using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� ���϶� �߰����� ���� �ȵ�, ���� ���� ���� �߰����� ���� ���� 

public class Jump2 : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 5f;

    bool jDown;
    bool isJumping; // ���� ������ ���θ� ��Ÿ���� ���� ���� �߰�

    Rigidbody rigid;
    Vector3 moveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();      
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) 
        {
            isJumping = true; // ���� ������ ���� ����
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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