using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform Door2; // 움직일 오브젝트
    public Vector3 moveDirection = Vector3.right; // 움직일 방향
    public float moveSpeed = 50000f; // 움직임 속도

    private bool isMoving = false; // 오브젝트가 움직이고 있는지 여부

    private void OnMouseDown()
    {
        // 움직임 시작
        isMoving = true;
    }

    private void Update()
    {
        // 오브젝트가 움직이고 있다면
        if (isMoving)
        {
            Rigidbody rb = Door2.GetComponent<Rigidbody>();
            if (rb != null) // 움직일 오브젝트가 Rigidbody 컴포넌트를 가지고 있다면
            {
                rb.velocity = moveDirection * moveSpeed; // 움직임 시작
            }
        }
    }
}
