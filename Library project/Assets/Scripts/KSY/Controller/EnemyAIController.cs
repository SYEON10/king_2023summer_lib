using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public class Spawn
    {
        #region private
        private float _waitingTime = 3.0f;
        private float _height = 1.5f;
        private float _minInclusiveX = 450;
        private float _maxInclusiveX = 550;
        private float _minInclusiveZ = 450;
        private float _maxInclusiveZ = 550;
        
        private GameObject _enemyParent = null;

        private string _enemyPath;

        private void CreateEnemy()
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(_minInclusiveX, _maxInclusiveX),_height,UnityEngine.Random.Range(_minInclusiveZ, _maxInclusiveZ));
            GameObject obj = GameManager.Resources.Instantiate(_enemyPath, position, _enemyParent.transform);
            //Debug.Log("적이 생성되었습니다. ");
            GameManager.EnemyCount++;
        }
        
        #endregion
        public Spawn(string enemyPath, float minIncX, float maxIncX, float minIncZ, float maxIncZ, float waitingTime = 3.0f, float height = 1.5f)
        {
            _enemyPath = enemyPath;
            _height = height; 
            _minInclusiveX = minIncX;
            _maxInclusiveX = maxIncX;
            _minInclusiveZ = minIncZ;
            _maxInclusiveZ = maxIncZ;
            _waitingTime = waitingTime;

            _enemyParent = Util.GetOrCreateObject($"@EnemyParent_{Util.GetNameFromPath(enemyPath)}_({(minIncX + maxIncX) / 2} : {(minIncZ + maxIncZ) / 2})");
            CreateEnemy();
        }
        
        //1. 적을 일정 시간동안 스폰 -> Stop
        //2. 적을 일정 수만큼 스폰 -> 일정 수가 되면 끝
        //3. 적을 특정 이벤트 발생 시까지 스폰 -> Stop
        public IEnumerator EnemySpawner()
        {
            while (true)
            {
                CreateEnemy();
                yield return new WaitForSeconds(_waitingTime);
            }
        }
    }
    
    private List<Coroutine> _enemyCoroutine = new List<Coroutine>();
    private List<Spawn> Spawners = new List<Spawn>();
    
    private string _enemyPath = "MonsterPF/FlyingMonster_Close";
    
    public void Init()
    {
        _enemyPath = "MonsterPF/FlyingMonster_Close";
        //Spawners.Add(new Spawn(_enemyPath, -38.0f, -35.0f, -38.0f, -35.0f));
        //Spawners.Add(new Spawn(_enemyPath, 35.0f, 38.0f, -38.0f, -35.0f));
        Spawners.Add(new Spawn(_enemyPath, -38.0f, -35.0f, 35.0f, 38.0f));
        Spawners.Add(new Spawn(_enemyPath, 35.0f, 38.0f, 35.0f, 38.0f));

        for (int i = 0; i < Spawners.Count; i++)
        {
            _enemyCoroutine.Add(StartCoroutine(Spawners[i].EnemySpawner()));
        }
    }

    public void StopSpawning()
    {
        for (int i = 0; i < _enemyCoroutine.Count; i++)
            StopCoroutine(Spawners[i].EnemySpawner());
        _enemyCoroutine.Clear();
    }

    //옮길 수 있을 것 같음
    public void OnUpdate()
    {
        
    }
}
