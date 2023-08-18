using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultCameraRotation = new Vector3(0f, 0f, 0f);
    [SerializeField] private float _sensitivity = 5.0f;

    // 고정 플레이어 기준 카메라 회전에서 쓰이는 변수
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float a = -6f, b = -6f, c = -6f, d = 0f, e = 2.5f, f = 0f, g = 0.4f;

    private float rotationX;
    private float rotationY;

    private float scroll = 0.5f;
    private float targetScroll = 1f;

    private void Update()
    {

    }

    void LateUpdate()
    {
        CameraRotate_player();
        scroll = Mathf.Lerp(scroll, targetScroll, Time.deltaTime / g);
        transform.position = _playerTransform.position + (Vector3.Scale(transform.forward, new Vector3(a, b, c)) + new Vector3(d, e, f)) * scroll;
        // CameraRotate_Object();
    }

    void CameraRotate_player()
    {

        rotationY += Input.GetAxis("Mouse X") * _sensitivity;
        rotationX += Input.GetAxis("Mouse Y") * -1 * _sensitivity;

        rotationX = Mathf.Min(Mathf.Max(rotationX, -24f), 30f);

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);


        targetScroll -= Input.GetAxis("Mouse ScrollWheel");
        targetScroll = Mathf.Min(Mathf.Max(targetScroll, 1f), 3f);
    }
}