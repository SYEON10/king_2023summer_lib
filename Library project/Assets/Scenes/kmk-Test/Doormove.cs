using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    private EnemyAIController _enemy;
    
    public Transform door1Transform;
    public Transform door2Transform;
    public float slideDistance = 5.0f;
    public float slideSpeed = 2.0f;

    private Vector3 initial1Position;
    private Vector3 initial2Position;
    private Vector3 target1Position;
    private Vector3 target2Position;
    private bool isDoorOpen = false;

    private void Start()
    {
        initial1Position = door1Transform.position;
        initial2Position = door2Transform.position; 
        target1Position = initial1Position + Vector3.left * slideDistance;
        target2Position = initial2Position + Vector3.right * slideDistance;
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼 클릭을 감지합니다.
        if (Input.GetMouseButtonDown(1))
        {
            // 클릭된 위치의 스크린 좌표를 Ray로 변환합니다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ray가 어떤 콜라이더와 충돌했는지 확인합니다.
            if (Physics.Raycast(ray, out hit))
            {
                // 버튼을 클릭했을 때 문 상태를 변경합니다.
                if (hit.collider.gameObject.name == "Up Button" || hit.collider.gameObject.name == "Down Button")
                {
                    ToggleDoor();
                }
            }
        }
    }
    
    private void ToggleDoor()
    {
        isDoorOpen = !isDoorOpen;
        GameManager.Sound.Play("ElevatorSound", Define.Sound.Effect); // 엘베 사운드 삽입 
        
        _enemy = Util.GetOrCreateObject("@EnemyController").GetOrAddComponent<EnemyAIController>();
        _enemy.Init();
    }

    private void FixedUpdate()
    {
        // 문 상태에 따라 문을 열거나 닫습니다.
        Vector3 target2 = isDoorOpen ? target2Position : initial2Position;
        Vector3 target1 = isDoorOpen ? target1Position : initial1Position;
        door1Transform.position = Vector3.MoveTowards(door1Transform.position, target1, slideSpeed * Time.fixedDeltaTime);
        door2Transform.position = Vector3.MoveTowards(door2Transform.position, target2, slideSpeed * Time.fixedDeltaTime);
    }
}


