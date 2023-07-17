using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 점프 중일때 추가적인 점프 안됨, 땅에 닿을 때만 추가적인 점프 가능 

public class Jump2 : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 5f;

    bool jDown;
    bool isJumping; // 점프 중인지 여부를 나타내는 상태 변수 추가

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
            isJumping = true; // 점프 중으로 상태 변경
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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