using UnityEngine;

public class ArcJump : MonoBehaviour
{
    Vector3 target; 
    bool isJumping = false; 
    float jumpHeight = 3f; // ���� �ִ� ����
    float jumpDuration = 1.5f; // ���� ��ü �ð�
    float jumpTimer = 0f; // ���� ���� �� ���� �ð�
    Vector3 initialPosition; // ���� ���۵� ��ġ 

    void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);
            float distanceToPlane; 

            if (plane.Raycast(ray, out distanceToPlane))
            {
                target = ray.GetPoint(distanceToPlane); // distanceToPlane: �������� ��ġ
                isJumping = true; 
                initialPosition = transform.position; // ���� ��ġ ���� 
                jumpTimer = 0f; 
            }
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime; 

            if (jumpTimer <= jumpDuration)
            {
                float normalizedTime = jumpTimer / jumpDuration;
                // ������ ���� ���� (0: ���� �� ~1: �Ϸ�)
                float yOffset = jumpHeight * 4f * (normalizedTime - normalizedTime * normalizedTime);
                // ���� ������ ���� 
                transform.position = Vector3.Slerp(initialPosition, target, normalizedTime) + yOffset * Vector3.up;
            }
            else
            {
                // �̵��� �Ϸ�Ǹ� ���� ����
                isJumping = false;
            }
        }
    }
}
