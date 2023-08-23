using UnityEngine;
using UnityEngine.SceneManagement;

public class time : MonoBehaviour
{
    private bool isInteracting = false; // 플레이어와 상호작용 중인지 여부
    private float interactionTime = 0f; // 상호작용 시작 시간
    private const float maxInteractionTime = 60f; // 최대 제한시간 (1분)

    private void Update()
    {
        if (isInteracting)
        {
            float elapsedTime = Time.time - interactionTime;
            GameManager.Timer = maxInteractionTime - elapsedTime;

            if (GameManager.Timer <= 0f)
            {
                Debug.Log("Time's up!");
                isInteracting = false;
                NextSceneWithNum(); // 시간이 다 되었을 때 다음 씬으로 이동
            }
            else
            {
                int remainingTimeInt = Mathf.FloorToInt(GameManager.Timer); // 소수점 아래를 버림처리하여 정수로 변환
                Debug.Log("남은 시간 : " + remainingTimeInt + "초");
            }
        }
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player 태그와 충돌한 경우
        {
            isInteracting = true;
            interactionTime = Time.time;
            Debug.Log("카즈하 언니를 찾으러 가자!");
        }
    }

    public void NextSceneWithNum()
    {
        GameManager.Scene.LoadScene(Define.Scene.Boss);
    }
}