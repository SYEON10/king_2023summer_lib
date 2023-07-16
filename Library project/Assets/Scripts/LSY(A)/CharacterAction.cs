using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jump2 �� Jump3�� ������ ��ġ�� �κ��� ���� Jump3�� ���� ���ַ� �ϳ��� Ŭ������ ����
public class CharacterAction : MonoBehaviour
{
    [SerializeField] // [Serializable Field] �߰� 
    float jumpForce = 5f;

    float jumpDuration = 0.5f;
    float jumpStartTime;
    bool isJumping; // isJumping ���� (���� �������� ��Ÿ�� -> ���� ��: true, �ƴϸ�: false)

    Rigidbody rigid;
    Vector3 moveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (isJumping && Input.GetKeyDown(KeyCode.Space) && (Time.time - jumpStartTime < jumpDuration))
        {
            JumpHigher();
        }
    }

    void Jump()
    {
        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
        jumpStartTime = Time.time;
    }

    void JumpHigher()
    {
        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // ���� ������ ���� ���� �ƴ϶�� ǥ��
        }
    }
}
