using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump3 : MonoBehaviour
{
    [SerializeField] 
    float jumpForce = 5f;

    float jumpDuration = 0.5f;
    float jumpStartTime;
    bool isJumping; // isJumping 변수 (점프 중인지를 나타냄 -> 점프 중: true, 아니면: false)

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
        if (isJumping && Input.GetKeyDown(KeyCode.Space) && (Time.time - jumpStartTime < jumpDuration))
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
            isJumping = false;  // 땅에 닿으면 점프 중이 아니라고 표시
        }
    }
   
}