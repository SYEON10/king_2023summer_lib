using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis; //Input Axis 값을 받을 전역 변수 선언
    bool wDown;
    bool jDown;

    bool isJump; //점프를 하고 있습니까?
    bool isDodge; //회피하고있습니까?

    Vector3 moveVec;
    Animator anim;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("walk");
        jDown = Input.GetButton("Jump");
    }

    void Move()
    {
        if(!isDodge) {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        }

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("is run", moveVec != Vector3.zero);
        anim.SetBool("is walk", wDown);

        
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); // 움직인 방향으로 바라본다
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge) { // 점프는 움직이고 있지 않을 때 발동되도록
            rigid.AddForce(Vector3.up * 50, ForceMode.Impulse);
            anim.SetBool("is jump", true);
            anim.SetTrigger("do jump");
            isJump = true;
        }

    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge) {
            speed *= 2; //회피=이동속도의 2배가 되게 코드 짜기, 회피는 움직이고 있을 때 발동되도록
            anim.SetTrigger("do dodge");
            isDodge = true;

            Invoke("DodgeOut", 0.4f); //Invoke()함수로 시간차 함수 호출, ()안에 함수를 문자열 형태로 씀+뒷 숫자는 시간차

        }

    }

    void DodgeOut()
    {
        speed *=0.5f;
        isDodge = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor"){
            anim.SetBool("is jump", false);
            isJump = false;
        }
    }
}

// 다 만들고 나서 생각 해보니까 그냥 회피 키를 누르면 회피할 수 있게 만드는게 나을 거 같음. 실제로 해본 결과 움직이면서
// 쉬프트 개불편함
