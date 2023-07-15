using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject player; //따라다닐 오브젝트 지정 
    public float xmove = 0; //x 누적 움직인 양
    public float ymove = 0; //y 누적 움직인 
    public float distance = 5;
    public float toggleView = 1;

    void Update()
    {


        if (Input.GetKeyDown("1"))
        {
            toggleView = toggleView + 1;
            if (toggleView > 4)
                toggleView = 1;
        }


        if (toggleView == 1)
        {
            if (Input.GetMouseButton(1)) //우클릭, 드래그해서 화면 돌리기
            {
                xmove += Input.GetAxis("Mouse X"); //마우스의 좌우 이동량을 xmove에 누적
                ymove -= Input.GetAxis("Mouse Y"); //마우스의 상하 이동량을 ymove에 누적
            }
            transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); //이동량에 따라 카메라가 바라보는 방향 조정 , 드래그 속도가 느려서 *2했음 

            Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);
            transform.position = player.transform.position - transform.rotation * reverseDistance;
        }
        if (toggleView == 2)
        {
            Vector3 upperSide = new Vector3(0.0f, 10, 0.0f);

            transform.position = player.transform.position + upperSide;
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        if (toggleView == 3)
        {

            Vector3 rightSide = new Vector3(10, 0.0f, 0.0f);

            transform.position = player.transform.position + rightSide;
            transform.rotation = Quaternion.Euler(0, 270, 0);

        }

        if (toggleView == 4)
        {

            Vector3 leftSide = new Vector3(-10, 0.0f, 0.0f);

            transform.position = player.transform.position + leftSide;
            transform.rotation = Quaternion.Euler(0, 90, 0);

        }
    }
}