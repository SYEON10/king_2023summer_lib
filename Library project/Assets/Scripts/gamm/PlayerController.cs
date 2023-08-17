using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //이동이 있을 때만 작동하도록 한다 
        {
            var offset = Cam.transform.forward;
            offset.y = 0; // 카메라가 바라보는 정면의 앞방향 구하기 
            transform.LookAt(SelectPlayer.transform.position + offset); // 해당 방향을 플레이어가 바라보도록 함 / LookAt 은 현재 위치 기준이 아닌 절대적인 좌표 기준이기 때문에 현재 플레이어 위치 SelectPlayer.transform.position 위치에 앞 방향을 더해 주어야 합니다.
        } // 카메라가 바라보는 방향대로 플레이어도 바라보도록 조정해주는거임 , 플레이어가 보는 방향으로 wasd 이용해 이동할 수 있게 해줌. 
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