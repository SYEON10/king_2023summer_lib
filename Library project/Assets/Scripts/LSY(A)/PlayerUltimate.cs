using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimate : MonoBehaviour
{
    Rigidbody rb;
    float _speed = 10f;
    float distanceThreshold = 5f;
    bool canAttack = true;
    float CoolTime = 5f;
    float LeftCoolTime = 0f;

    float arcJumpCooldown = 2f;
    float arcJumpDuration = 1.5f; 
    float arcJumpTimer = 0f; 
    float arcJumpHeight = 5f; 
    bool isArcJumping = false;

    Vector3 arcJumpTarget;
    int ultimateCharges = 3; // ±Ã±Ø±â È½¼ö 

    public float P_LeftCoolTime { get { return LeftCoolTime; } }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleArcJump();

        if (canAttack)
        {
            Attack();
        }
        else
        {
            HandleCooldown();
        }

        if (Input.GetKeyDown(KeyCode.Q) && ultimateCharges > 0)
        {
            UseUltimate();
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.4f);
            transform.position += moveDirection * Time.deltaTime * _speed;
        }
    }

    void HandleArcJump()
    {
        if (arcJumpTimer > 0f)
        {
            arcJumpTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && arcJumpTimer <= 0f)
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

    void Attack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= distanceThreshold)
            {
                enemy.SetActive(false);
                LeftCoolTime = CoolTime;
                canAttack = false;
                StartCoroutine(AttackCoolTime());
                break;
            }
        }
    }

    void HandleCooldown()
    {
        if (LeftCoolTime > 0f)
        {
            LeftCoolTime -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
            LeftCoolTime = 0f;
        }
    }

    void UseUltimate()
    {
        ultimateCharges--;
        Debug.Log("Remaining Ultimate charges: " + ultimateCharges); // ±Ã±Ø±â »ç¿ë ÈÄ ³²Àº È½¼ö Ç¥½Ã
        arcJumpTimer = arcJumpCooldown;
        isArcJumping = true;
        arcJumpTarget = GetMouseClickPosition();
    }

    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(CoolTime);
        canAttack = true;
    }
}