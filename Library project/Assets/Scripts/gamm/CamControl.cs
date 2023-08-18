using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject player; //따라다닐 오브젝트 지정 
    public float xmove = 0; //x 누적 움직인 양
    public float ymove = 0; //y 누적 움직인 

    public Transform target;
    public Vector3 offset;

    public float SmoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private int toggleView = 3; //현재 1인칭인지, 3인칭인지 판단. 처음에는 3인칭 


    void Update()
    {
        if (Input.GetMouseButtonDown(2)) //휠클릭, 1/3인칭 시점 전환
            toggleView = 4 - toggleView; //1>3 클릭때마다 변함 

        if (toggleView == 1) //1인칭일때 
        {
            xmove += Input.GetAxis("Mouse X"); //마우스의 좌우 이동량을 xmove에 누적
            ymove -= Input.GetAxis("Mouse Y"); //마우스의 상하 이동량을 ymove에 누적

            transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); //이동량에 따라 카메라가 바라보는 방향 조정 , 드래그 속도가 느려서 *2했음 
            Vector3 reverseDistance = new Vector3(0.0f, 5.0f, 1.0f); // 카메라가 바라보는 앞방향은 Z 축, 이동량에 따른 Z 축방향의 벡터를 구함 
            transform.position = player.transform.position + transform.rotation * reverseDistance; // 플레이어의 위치에서 카메라가 바라보는 방향에 벡터값을 적용한 상대 좌표를 차감
        }
        else if (toggleView == 3)
        {
            if (Input.GetMouseButton(1))
            {
                xmove += Input.GetAxis("Mouse X"); //마우스의 좌우 이동량을 xmove에 누적
                ymove -= Input.GetAxis("Mouse Y"); //마우스의 상하 이동량을 ymove에 누적
            }
            transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); //이동량에 따라 카메라가 바라보는 방향 조정 , 드래그 속도가 느려서 *2했음 
            Vector3 reverseDistance1 = new Vector3(0.0f, 6.0f, -20.0f);
            transform.position = player.transform.position + transform.rotation * reverseDistance1;
        }
    }
}
