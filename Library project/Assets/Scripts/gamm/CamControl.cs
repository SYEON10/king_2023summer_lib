using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject player; //����ٴ� ������Ʈ ���� 
    public float xmove = 0; //x ���� ������ ��
    public float ymove = 0; //y ���� ������ 
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
            if (Input.GetMouseButton(1)) //��Ŭ��, �巡���ؼ� ȭ�� ������
            {
                xmove += Input.GetAxis("Mouse X"); //���콺�� �¿� �̵����� xmove�� ����
                ymove -= Input.GetAxis("Mouse Y"); //���콺�� ���� �̵����� ymove�� ����
            }
            transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); //�̵����� ���� ī�޶� �ٶ󺸴� ���� ���� , �巡�� �ӵ��� ������ *2���� 

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