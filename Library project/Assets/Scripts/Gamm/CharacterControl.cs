using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public GameObject Cam;
    public CharacterController SelectPlayer;
    public float Speed;
    private float Gravity;
    private Vector3 MoveDir;

    void Start()
    {
        Speed = 5.0f;
        Gravity = 10.0f;
        MoveDir = Vector3.zero;
    }

    void Update()
    {
        if (SelectPlayer == null) return;



        if (SelectPlayer.isGrounded)//땅에붙어있으면, 추락하는 중에는 이동X
        {

            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // 키보드에 따른 X, Z 축 이동방향을 새로 결정
            MoveDir = SelectPlayer.transform.TransformDirection(MoveDir); // 오브젝트가 바라보는 앞방향으로 이동방향을 돌려서 조정
            MoveDir *= Speed; // 속도를 곱해서 적용
        }
        else   //허공이면
        {
            MoveDir.y -= Gravity * Time.deltaTime;//중력의 영향, 아래로 하강
        }
        SelectPlayer.Move(MoveDir * Time.deltaTime); //실제 이동 처리
    }

}