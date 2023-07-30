using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform;
    public float distanceThreshold = 10f; // �Ÿ� �Ӱ谪
    public float moveSpeed = 30f;
    public float directionChangeInterval = 3f; // ������ �ٲٴ� �ֱ�

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
        // �÷��̾�� ������ �Ÿ��� ���
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log("Distance: " + distance);

        // �Ÿ��� �Ӱ谪���� ������ ���� �����ϰ� ����
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

        // ���� �ֱ⸶�� ���� ������ ����
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            SetRandomDirection();
            timeSinceLastDirectionChange = 0f;
        }

        // ���� ���� �������� �̵���Ŵ
        Move();
        Debug.Log("isTransparent: " + isTransparent); // ����� �޽����� ���� ���� ���
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
        // 0������ 360�� �߿��� ������ ���� ����
        float randomAngle = Random.Range(0f, 360f);
        // ������ ������ ���� ���� ����
        Vector3 randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;
        // y���� 0���� �����Ͽ� �� �������� �����̵��� ����
        randomDirection.y = 0f;

        // ���� ������ ���ο� ������ �������� ����
        transform.forward = randomDirection;
    }

    private void Move()
    {
        // ���� ���� �������� �̵���Ŵ
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
