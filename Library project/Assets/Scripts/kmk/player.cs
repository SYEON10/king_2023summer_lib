using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis; //Input Axis 값을 받을 전역 변수 선언
    bool wDown;

    Vector3 moveVec;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("is run", moveVec != Vector3.zero);
        anim.SetBool("is walk", wDown);

        transform.LookAt(transform.position + moveVec); // 움직인 방향으로 바라본다
    }
}
