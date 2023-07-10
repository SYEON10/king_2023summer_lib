using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump2 : MonoBehaviour
{
    bool jDown;
    bool isJumping; // 점프 중인지 여부를 나타내는 상태 변수 추가

    Rigidbody rigid;
    Vector3 moveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();      //Rigidbody 컴포넌트를 받아옴
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // 추가된 조건문
        {
            isJumping = true; // 점프 중으로 상태 변경
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 바닥에 닿았을 때
        {
            isJumping = false; // 점프 중인 상태 해제
        }
    }
}