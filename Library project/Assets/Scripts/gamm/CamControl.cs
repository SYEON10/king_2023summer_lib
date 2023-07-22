using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject player; //따라다닐 오브젝트 지정 
    public float xmove = 0; //x 누적 움직인 양
    public float ymove = 0; //y 누적 움직인 
    public float distance = 5;
    public enum CameraView { player1, player3, upperSideCam, rigtSideCam, leftSideCam }
    public int camera = 0;
    public KeyCode cameraKey = KeyCode.Alpha1; // 기본 카메라 키를 1로 설정




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("키가 랜덤으로 변경됩니다");
            cameraKey = (KeyCode)Random.Range((int)KeyCode.A, (int)KeyCode.Z + 1);
        }


        if (Input.GetMouseButton(0))
        {
            xmove += Input.GetAxis("Mouse X");
            ymove -= Input.GetAxis("Mouse Y");
            transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0);

        }

 

        if (Input.GetKeyDown(cameraKey))
        {
            camera = camera + 1;
            if (camera == 5) { camera = 0; }
        }



        switch ((CameraView)camera)
        {
            case CameraView.player1:
                Vector3 reverseDistance1 = new Vector3(0.0f, 0.4f, 0.2f); // 카메라가 바라보는 앞방향은 Z 축, 이동량에 따른 Z 축방향의 벡터를 구함 
                transform.position = player.transform.position + transform.rotation * reverseDistance1;
                break;
            case CameraView.player3:
                Vector3 reverseDistance3 = new Vector3(0.0f, 0.0f, distance);
                transform.position = player.transform.position - transform.rotation * reverseDistance3;
                break;
            case CameraView.upperSideCam:
                Vector3 upperSide = new Vector3(0.0f, 10, 0.0f);
                transform.position = player.transform.position + upperSide;
                transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case CameraView.rigtSideCam:
                Vector3 rightSide = new Vector3(10, 0.0f, 0.0f);
                transform.position = player.transform.position + rightSide;
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case CameraView.leftSideCam:
                Vector3 leftSide = new Vector3(-10, 0.0f, 0.0f);
                transform.position = player.transform.position + leftSide;
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }
    }
}
