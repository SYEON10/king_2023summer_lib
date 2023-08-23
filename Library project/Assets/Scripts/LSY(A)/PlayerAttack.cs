using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    Rigidbody rb;
    float _speed = 10f;
    public bool canAttack = true;
    float CoolTime = 5f; // 재공격 가능할 때까지의 시간 
    float LeftCoolTime = 0f; // 쿨타임 끝나기까지 남은 시간

    public GameObject elevator;

    [SerializeField] GameObject particles;

    public float P_LeftCoolTime
    {
        get { return LeftCoolTime; }
    }

    float arcJumpCooldown = 2f;
    float arcJumpDuration = 1.5f;
    float arcJumpTimer = 0f;
    float arcJumpHeight = 5f;
    bool isArcJumping = false;

    Vector3 arcJumpTarget;

    public int ultimateCharges = 3; // 궁극기 횟수 
    //public Text Count; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particles.gameObject.SetActive(false); // 초기에 파티클은 비활성화

    }

    void Update()
    {
        MovePlayer();
        HandleAttack();
        HandleArcJump();
        Player_Elevator_Check();

        if (Input.GetKeyDown(KeyCode.Q) && ultimateCharges > 0)
        {
            UseUltimate();
        }
    }

    void Player_Elevator_Check()
    {
        if (GameManager.Scene.CurrentScene.SceneType == Define.Scene.Game)
        {
            if (gameObject.transform.position.y < elevator.transform.position.y - 10.0f)
            {
                GameManager.GameOver();
            }
        }
    }

    void MovePlayer()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.4f);
            transform.position += moveDirection.normalized * Time.deltaTime * _speed;
        }
    }

    void HandleAttack()
    {
        if (!canAttack)
        {
            if (LeftCoolTime > 0f) // 쿨타임 이내 
            {
                LeftCoolTime -= Time.deltaTime;
            }
            else // 쿨타임 종료 
            {
                canAttack = true;
                LeftCoolTime = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (canAttack)
            {
                GameManager.Sound.Play("CloseAttack", Define.Sound.Effect);
                GameManager.EnemyCount--;
                particles.gameObject.SetActive(true); // 파티클 활성화
                StartCoroutine(AttackEnemy(other.gameObject));
            }
            else
            {
                GameManager.GameOver();
                GameManager.PlayerAlive = false;
            }
        }
    }

    void HandleArcJump()
    {
        if (arcJumpTimer > 0f)
        {
            arcJumpTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && arcJumpTimer <= 0f && ultimateCharges > 0)
        {
            arcJumpTimer = arcJumpCooldown;
            isArcJumping = true;
            arcJumpTarget = GetMouseClickPosition();

        }

        if (isArcJumping)
        {
            PerformArcJump();
        }
    }

    Vector3 GetMouseClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distanceToPlane;

        if (plane.Raycast(ray, out distanceToPlane))
        {
            return ray.GetPoint(distanceToPlane);
        }

        return Vector3.zero;
    }

    void PerformArcJump()
    {
        Vector3 initialPosition = transform.position;
        float timer = 0f;

        while (timer < arcJumpDuration)
        {
            float normalizedTime = timer / arcJumpDuration;
            float yOffset = arcJumpHeight * 4f * (normalizedTime - normalizedTime * normalizedTime);
            transform.position = Vector3.Slerp(initialPosition, arcJumpTarget, normalizedTime) + yOffset * Vector3.up;
            timer += Time.deltaTime;
        }

        isArcJumping = false;
    }

    void UseUltimate()
    {
        ultimateCharges--;
        Debug.Log("Remaining Ultimate charges: " + ultimateCharges); // 궁극기 사용 후 남은 횟수 표시
        arcJumpTimer = arcJumpCooldown;
        isArcJumping = true;
        arcJumpTarget = GetMouseClickPosition();
    }

    public void PlayerDead()
    {
        GameManager.Sound.Play("GameOver", Define.Sound.Effect);
        GameManager.PauseGame();
        GameManager.GameOver();
        GameManager.PlayerAlive = false;
    }

    IEnumerator AttackEnemy(GameObject enemy)
    {
        canAttack = false;
        LeftCoolTime = CoolTime;

        enemy.SetActive(false); // 적 죽음

        yield return new WaitForSeconds(CoolTime); // 쿨타임 대기

        particles.gameObject.SetActive(false); // 파티클 비활성화
        canAttack = true;
    }
}