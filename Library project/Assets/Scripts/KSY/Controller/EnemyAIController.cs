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
            _enemyParent.tag = "Spawner";
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

    private string[] _flyingEnemyPath =
    {
        "MonsterPF/FlyingMonster_Close",
        "MonsterPF/FlyingMonster_Far"
    };

    private string[] _walkingEnemyPath =
    {
        "MonsterPF/WalkingMonster_Close",
        "MonsterPF/WalkingMonster_Far"
    };
    
    public void Init()
    {
        if (GameManager.Scene.CurrentScene.SceneType == Define.Scene.Game) { SpawnInGame(); }
        else if (GameManager.Scene.CurrentScene.SceneType == Define.Scene.Boss) { SpawnInBoss(); }

        for (int i = 0; i < Spawners.Count; i++) { _enemyCoroutine.Add(StartCoroutine(Spawners[i].EnemySpawner())); }
    }

    private void SpawnInGame()
    {
        Debug.Log("실행됨");
        Spawners.Add(new Spawn(_walkingEnemyPath[0], -25.0f, -18.0f, 33.0f, 38.0f, 10.0f, 0.0f));
        Spawners.Add(new Spawn(_walkingEnemyPath[0], 30.0f, 38.0f, -40.0f, -35.0f, 10.0f, -35.0f));
        Spawners.Add(new Spawn(_walkingEnemyPath[1], 30f, 37.0f, 33.0f, 38.0f, 10.0f, -70.0f));
        Spawners.Add(new Spawn(_walkingEnemyPath[1], -38.0f, -33.0f, -40.0f, -35.0f, 10.0f, -105.0f));
    }

    private void SpawnInBoss()
    {
        Spawners.Add(new Spawn(_walkingEnemyPath[0], -38.0f, -33.0f, 33.0f, 38.0f, 10.0f));
        Spawners.Add(new Spawn(_walkingEnemyPath[0], 33.0f, 38.0f, 33.0f, 38.0f, 10.0f));
        Spawners.Add(new Spawn(_walkingEnemyPath[1], -38.0f, -33.0f, 33.0f, 38.0f, 10.0f));
        Spawners.Add(new Spawn(_walkingEnemyPath[1], 33.0f, 38.0f, 33.0f, 38.0f, 10.0f));
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
