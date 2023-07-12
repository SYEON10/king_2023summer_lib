using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using System;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] public float _speed = 5.0f; 
	[SerializeField] protected CreatureState _state = CreatureState.Idle;

	public Vector3 CurrentPos { get; set; } = new Vector3(0, 0, 0);
	[SerializeField] private Vector3 _destPos;
	[SerializeField] private GameObject _target;
	[SerializeField] private double _searchRange = 9.0;
	
	private Coroutine _coPatrol;
	private Coroutine _coSearch;

	public CreatureState State
	{
		get { return _state; }
		set
		{
			if (_state == value)
				return;

			_state = value;

			if (_coPatrol != null)
			{
				StopCoroutine(_coPatrol);
				_coPatrol = null;
			}
			if (_coSearch != null)
			{
				StopCoroutine(_coSearch);
				_coSearch = null;
			}
		}
	}
	
	protected  void Init()
	{
		State = CreatureState.Idle;
		_speed = 3.0f;
	}
	
	void Update()
	{
		UpdateController();
	}

	protected virtual void UpdateController()
	{
		switch (State)
		{
			case CreatureState.Idle:
				UpdateIdle();
				break;
			case CreatureState.Moving:
				UpdateMoving();
				break;
			case CreatureState.Skill:
				// UpdateSkill();
				break;
			case CreatureState.Dead:
				// UpdateDead();
				break;
		}
	}

	protected void UpdateIdle()
	{
		if (_coPatrol == null)
		{
			_coPatrol = StartCoroutine("CoPatrol");
		}
		if (_coSearch == null)
		{
			// _coSearch = StartCoroutine("CoSearch");
		}
	}
	
	protected virtual void UpdateMoving()
	{
		Vector3 moveDir = _destPos - transform.position;

		// 도착 여부 체크
		float dist = moveDir.magnitude;
		if (dist < _speed * Time.deltaTime)
		{
			transform.position = _destPos;
			State = CreatureState.Idle;
		}
		else
		{
			transform.position += moveDir.normalized * _speed * Time.deltaTime;
			State = CreatureState.Moving;
		}
	}
	
	IEnumerator CoPatrol()
	{
		int waitSeconds = UnityEngine.Random.Range(1, 4);
		yield return new WaitForSeconds(waitSeconds);

		for (int i = 0; i < 10; i++)
		{
			int xRange = UnityEngine.Random.Range(-5, 6);
			int zRange = UnityEngine.Random.Range(-5, 6);
			Vector3 randPos = transform.position + new Vector3(xRange, 0, zRange);

			_destPos = randPos;
			State = CreatureState.Moving;
			yield break;
		}
		State = CreatureState.Idle;
	}
	
	IEnumerator CoSearch()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);

			Transform player = GameObject.FindWithTag("Player").transform;
			Vector3 deltaPos = (player.position - transform.position);
			double deltaDist = Math.Pow(deltaPos.x, 2) + Math.Pow(deltaPos.z, 2);
			if (deltaDist > _searchRange)
			{
				continue;
			}
			_target = player.gameObject;
		}
	}
}
