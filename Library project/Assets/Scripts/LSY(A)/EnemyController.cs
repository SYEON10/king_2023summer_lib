using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform;
    public float distanceThreshold = 10f; // 거리 임계값
    public float moveSpeed = 30f;
    public float directionChangeInterval = 3f; // 방향을 바꾸는 주기

    private Renderer rend;
    private bool isTransparent = false;
    private float timeSinceLastDirectionChange;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        timeSinceLastDirectionChange = 0f;
    }

    private void Update()
    {
        // 플레이어와 적과의 거리를 계산
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log("Distance: " + distance);

        // 거리가 임계값보다 작으면 적을 투명하게 만듦
        if (distance < distanceThreshold)
        {
            if (!isTransparent)
            {
                SetTransparency(true);
                isTransparent = true;
            }
        }
        else
        {
            if (isTransparent)
            {
                SetTransparency(false);
                isTransparent = false;
            }
        }

        // 일정 주기마다 적의 방향을 변경
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            SetRandomDirection();
            timeSinceLastDirectionChange = 0f;
        }

        // 적을 현재 방향으로 이동시킴
        Move();
        Debug.Log("isTransparent: " + isTransparent); // 디버그 메시지로 투명 여부 출력
    }

    private void SetTransparency(bool transparent)
    {
        if (rend != null)
        {
            Color color = rend.material.color;
            color.a = transparent ? 0.5f : 1f;
            rend.material.color = color;
        }
    }

    private void SetRandomDirection()
    {
        // 0도부터 360도 중에서 랜덤한 각도 선택
        float randomAngle = Random.Range(0f, 360f);
        // 랜덤한 각도로 방향 벡터 생성
        Vector3 randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;
        // y값은 0으로 고정하여 땅 위에서만 움직이도록 설정
        randomDirection.y = 0f;

        // 적의 방향을 새로운 랜덤한 방향으로 설정
        transform.forward = randomDirection;
    }

    private void Move()
    {
        // 적을 현재 방향으로 이동시킴
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
