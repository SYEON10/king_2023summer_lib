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
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //�̵��� ���� ���� �۵��ϵ��� �Ѵ� 
        {
            var offset = Cam.transform.forward;
            offset.y = 0; // ī�޶� �ٶ󺸴� ������ �չ��� ���ϱ� 
            transform.LookAt(SelectPlayer.transform.position + offset); // �ش� ������ �÷��̾ �ٶ󺸵��� �� / LookAt �� ���� ��ġ ������ �ƴ� �������� ��ǥ �����̱� ������ ���� �÷��̾� ��ġ SelectPlayer.transform.position ��ġ�� �� ������ ���� �־�� �մϴ�.
        } // ī�޶� �ٶ󺸴� ������ �÷��̾ �ٶ󺸵��� �������ִ°��� , �÷��̾ ���� �������� wasd �̿��� �̵��� �� �ְ� ����. 
        if (SelectPlayer.isGrounded)//�����پ�������, �߶��ϴ� �߿��� �̵�X
        {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // Ű���忡 ���� X, Z �� �̵������� ���� ����
            MoveDir = SelectPlayer.transform.TransformDirection(MoveDir); // ������Ʈ�� �ٶ󺸴� �չ������� �̵������� ������ ����
            MoveDir *= Speed; // �ӵ��� ���ؼ� ����
        }
        else   //����̸�
        {
            MoveDir.y -= Gravity * Time.deltaTime;//�߷��� ����, �Ʒ��� �ϰ�
        }
        SelectPlayer.Move(MoveDir * Time.deltaTime); //���� �̵� ó��
    }

}