using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour

{
    public Rigidbody floorRigidbody; // floor의 Rigidbody 컴포넌트
    public float fallSpeed = 5f; // 떨어지는 속도

    private bool isFalling = false; // floor가 떨어지고 있는지 여부

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌하면
        {
            isFalling = true; // floor가 떨어지도록 상태 변경
        }
    }

    private void Update()
    {
        if (isFalling)
        {
            // Rigidbody를 사용하여 floor를 떨어지게 합니다.
            Vector3 fallDirection = Vector3.down; // 아래로 떨어지도록 설정
            Vector3 fallVelocity = fallDirection * fallSpeed * Time.deltaTime;
            floorRigidbody.velocity = fallVelocity;
        }
    }
}
