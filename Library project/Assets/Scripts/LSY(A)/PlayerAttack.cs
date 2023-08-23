using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    Rigidbody rb;
    float _speed = 10f;
    public bool canAttack = true;
    float CoolTime = 5f; // ����� ������ �������� �ð� 
    float LeftCoolTime = 0f; // ��Ÿ�� ��������� ���� �ð�

    [SerializeField] GameObject particles;
    public float P_LeftCoolTime { get { return LeftCoolTime; } }

    float arcJumpCooldown = 2f;
    float arcJumpDuration = 1.5f;
    float arcJumpTimer = 0f;
    float arcJumpHeight = 5f;
    bool isArcJumping = false;

    public GameObject Cam;
    public CharacterController SelectPlayer;
    public float Speed = 5.0f;
    private Vector3 MoveDir = Vector3.zero;

    Vector3 arcJumpTarget;
    public int ultimateCharges = 3; // �ñر� Ƚ�� 
    //public Text Count; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particles.gameObject.SetActive(false); // �ʱ⿡ ��ƼŬ�� ��Ȱ��ȭ

    }

    void Update()
    {
        MovePlayer();
        HandleAttack();
        HandleArcJump();

        if (Input.GetKeyDown(KeyCode.Q) && ultimateCharges>0)
        {
            UseUltimate();
        }
    }

    void MovePlayer()
    {

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            var offset = Cam.transform.forward;
            offset.y = 0;
            transform.LookAt(SelectPlayer.transform.position + offset);
        }

            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDir = SelectPlayer.transform.TransformDirection(MoveDir);
            MoveDir *= Speed;

        SelectPlayer.Move(MoveDir * Time.deltaTime);

    }

        void HandleAttack()
    {
        if (!canAttack)
        {
            if (LeftCoolTime > 0f) // ��Ÿ�� �̳� 
            {
                LeftCoolTime -= Time.deltaTime;
            }
            else // ��Ÿ�� ���� 
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
                particles.gameObject.SetActive(true); // ��ƼŬ Ȱ��ȭ
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
        Debug.Log("Remaining Ultimate charges: " + ultimateCharges); // �ñر� ��� �� ���� Ƚ�� ǥ��
        arcJumpTimer = arcJumpCooldown;
        isArcJumping = true;
        arcJumpTarget = GetMouseClickPosition();
    }

    public void PlayerDead()
    {
        GameManager.GameOver();
        GameManager.PlayerAlive = false;
        GameManager.Sound.Play("PlayerDead", Define.Sound.Effect);
        
    }



    IEnumerator AttackEnemy(GameObject enemy)
    {
        canAttack = false;
        LeftCoolTime = CoolTime;

        enemy.SetActive(false); // �� ����

        yield return new WaitForSeconds(CoolTime); // ��Ÿ�� ���

        particles.gameObject.SetActive(false); // ��ƼŬ ��Ȱ��ȭ
        canAttack = true;
    }
}
