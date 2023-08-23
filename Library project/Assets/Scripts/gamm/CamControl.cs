using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트를 연결할 변수
    public float xmove = 0; // x 축 회전값
    public float ymove = 0; // y 축 회전값

    public Transform target;
    public Vector3 offset;

    public float SmoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private int toggleView = 3; // 카메라 보기 모드 토글 (1 or 3)

    void Update()
    {
        if (Input.GetMouseButtonDown(2)) // 마우스 중간 버튼 클릭시
            toggleView = 4 - toggleView; // 1을 3으로, 3을 1으로 변경하여 보기 모드 토글

        if (toggleView == 1) // 보기 모드가 1일 때
        {
            xmove += Input.GetAxis("Mouse X"); // 마우스 X 이동에 따라 x 회전값 변경
            ymove -= Input.GetAxis("Mouse Y"); // 마우스 Y 이동에 따라 y 회전값 변경

            transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); // 회전값을 적용하여 카메라 회전
            Vector3 reverseDistance = new Vector3(0.0f, 5.0f, 1.0f);
            transform.position = player.transform.position + transform.rotation * reverseDistance; // 플레이어 주위로 이동하여 시점 변경
        }
        else if (toggleView == 3) // 보기 모드가 3일 때
        {
            if (Input.GetMouseButton(1)) // 마우스 오른쪽 버튼이 눌린 경우
            {
                xmove += Input.GetAxis("Mouse X"); // 마우스 X 이동에 따라 x 회전값 변경
                ymove -= Input.GetAxis("Mouse Y"); // 마우스 Y 이동에 따라 y 회전값 변경
            }
            transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); // 회전값을 적용하여 카메라 회전
            Vector3 reverseDistance1 = new Vector3(0.0f, 6.0f, -20.0f);
            transform.position = player.transform.position + transform.rotation * reverseDistance1; // 플레이어 주위로 이동하여 시점 변경
        }
    }
}
