using UnityEngine;

public class ArcJump : MonoBehaviour
{
    Vector3 target; 
    bool isJumping = false; 
    float jumpHeight = 3f; // 점프 최대 높이
    float jumpDuration = 1.5f; // 점프 전체 시간
    float jumpTimer = 0f; // 점프 시작 후 지난 시간
    Vector3 initialPosition; // 점프 시작된 위치 

    void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);
            float distanceToPlane; 

            if (plane.Raycast(ray, out distanceToPlane))
            {
                target = ray.GetPoint(distanceToPlane); // distanceToPlane: 교차점의 위치
                isJumping = true; 
                initialPosition = transform.position; // 현재 위치 저장 
                jumpTimer = 0f; 
            }
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime; 

            if (jumpTimer <= jumpDuration)
            {
                float normalizedTime = jumpTimer / jumpDuration;
                // 점프의 진행 상태 (0: 진행 중 ~1: 완료)
                float yOffset = jumpHeight * 4f * (normalizedTime - normalizedTime * normalizedTime);
                // 점프 동작의 높이 
                transform.position = Vector3.Slerp(initialPosition, target, normalizedTime) + yOffset * Vector3.up;
            }
            else
            {
                // 이동이 완료되면 점프 종료
                isJumping = false;
            }
        }
    }
}
