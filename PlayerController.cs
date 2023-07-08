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
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            var offset = Cam.transform.forward;
            offset.y = 0;
            transform.LookAt(SelectPlayer.transform.position + offset);
        }

        if (SelectPlayer.isGrounded)//땅에붙어있으면
        {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDir = SelectPlayer.transform.TransformDirection(MoveDir);
            MoveDir *= Speed;
        }
        else   //허공이면
        {
            MoveDir.y -= Gravity * Time.deltaTime;
        }
        SelectPlayer.Move(MoveDir * Time.deltaTime);
    }
}