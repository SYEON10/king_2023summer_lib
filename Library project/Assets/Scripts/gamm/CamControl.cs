using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject player; //����ٴ� ������Ʈ ���� 
    public float xmove = 0; //x ���� ������ ��
    public float ymove = 0; //y ���� ������ 
   
    public float SmoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private int toggleView = 3; //���� 1��Ī����, 3��Ī���� �Ǵ�. ó������ 3��Ī 


    void Update()
    {
        
            xmove += Input.GetAxis("Mouse X"); //���콺�� �¿� �̵����� xmove�� ����
            ymove -= Input.GetAxis("Mouse Y"); //���콺�� ���� �̵����� ymove�� ����
        
        transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); //�̵����� ���� ī�޶� �ٶ󺸴� ���� ���� , �巡�� �ӵ��� ������ *2���� 

        if (Input.GetMouseButtonDown(2)) //��Ŭ��, 1/3��Ī ���� ��ȯ
            toggleView = 4 - toggleView; //1>3 Ŭ�������� ���� 

        if (toggleView == 1) //1��Ī�϶� 
        {
            Vector3 reverseDistance = new Vector3(0.0f, 5.0f, 0.2f); // ī�޶� �ٶ󺸴� �չ����� Z ��, �̵����� ���� Z ������� ���͸� ���� 
            transform.position = player.transform.position + transform.rotation * reverseDistance; // �÷��̾��� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� ����
        }
        else if (toggleView == 3)
        {
            Vector3 reverseDistance = new Vector3(0.0f, -4.0f, 20.0f);
            transform.position = player.transform.position - transform.rotation * reverseDistance;
        }
    }
}
