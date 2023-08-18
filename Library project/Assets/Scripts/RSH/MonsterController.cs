using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static Define;
using UnityEngine.UI;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterController : BaseController
{
	private GameObject player;
	[SerializeField] float _scanRange = 100000;
	[SerializeField] float _attackRange = 10f;
	[SerializeField] private float _moveSpeed = 5f;

	[SerializeField] private MonsterType _monsterType = MonsterType.Unknown;
	[SerializeField] private AttackType _attackType = AttackType.Unkown;
	[SerializeField] private GameObject _bulletPF;
	[SerializeField] private bool isAttacking = false;
	[SerializeField] private Sprite[] _monsterSprites;
	
	private float _skillCoolTime = 2.0f;

    public override void Init()
    {
		player = GameObject.FindWithTag("Player");
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = _monsterSprites[Random.Range(0, _monsterSprites.Length)];
    }

	protected override void UpdateIdle()
	{
		if (player == null)
			return;

		float distance = (player.transform.position - transform.position).magnitude;
		if (distance <= _scanRange)
		{
			_lockTarget = player;
			State = Define.State.Moving;
		}
	}

	protected override void UpdateMoving()
	{
		// 플레이어가 내 사정거리보다 가까우면 공격
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= _attackRange)
			{
				// TODO 
				State = Define.State.Skill;
				return;
			}
		}
		// 이동
		Vector3 moveDir = _destPos - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 20 * Time.deltaTime);
		
		if (moveDir.magnitude < _moveSpeed * Time.deltaTime)
		{
			transform.position = _destPos;
			State = Define.State.Idle;
		}
		else
		{
			transform.position += moveDir.normalized * _moveSpeed * Time.deltaTime;
			State = Define.State.Moving;
		}
	}

	protected override void UpdateSkill()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);

			switch (_attackType)
			{
				case AttackType.Close:
					CloseAttack();
					break;
				case AttackType.Far:
					FarAttack(quat);
					break;
				default:
					State = State.Idle;
					break;
			}
		}
	}

	private void CloseAttack()
	{
		if (_attackType != AttackType.Close || isAttacking)
			return;
		
		isAttacking = true;
		transform.DOMove(_destPos, 1.0f);
		StartCoroutine(WaitForCoolTime());
	}

	private void FarAttack(Quaternion attackDir)
	{
		if (_attackType != AttackType.Far || isAttacking)
			return;
		
		// Bullet 생성
		isAttacking = true;
		Instantiate(_bulletPF, transform.position, attackDir);
		StartCoroutine(WaitForCoolTime());
	}

	IEnumerator WaitForCoolTime()
	{
		yield return new WaitForSeconds(_skillCoolTime);
		isAttacking = false;
		State = State.Idle;
	}
	
	// TODO
	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject == player)
		{
			return;
		}

		if (true)
		{
			MonsterDie();
		}
		else
		{
			//PlayerDie
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// 플레이어 총알 맞았을 때 Die 처리
		if (other.CompareTag("PlayerBullet"))
		{
			MonsterDie();
		}
	}

	public void MonsterDie()
	{
		Destroy(gameObject);
	}
}
