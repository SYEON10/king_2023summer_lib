using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField]
    float _speed = 10.0f;

    private Vector3 lastPosition;

    private void Start()
    {
        audioSource.clip = audioClip;
        // Inspector에서 play on awake 옵션 체크 해제 안하면 자동 재생됨
        lastPosition = transform.position;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        if (movement != Vector3.zero)
        {
            // 플레이어가 움직이는 중이면 소리를 재생합니다.
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // 움직임 벡터의 방향으로 회전합니다.
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
        }
        else
        {
            // 플레이어가 멈추면 소리를 멈춥니다.
            audioSource.Stop();
        }

        // 실제 움직임을 적용합니다.
        transform.Translate(movement.normalized * _speed * Time.deltaTime);

        // 현재 위치를 저장합니다.
        lastPosition = transform.position;
    }
}
