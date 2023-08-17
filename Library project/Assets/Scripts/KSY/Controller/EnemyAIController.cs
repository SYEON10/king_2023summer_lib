using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    class SpawnPoint
    {
        private float _waitingTime = 3.0f;
        private int _minInclusive_X = 450;
        private int _maxInclusive_X = 550;
        private int _minInclusive_Z = 450;
        private int _maxInclusive_Z = 550;
        private float _height = 1.0f;
        
        private GameObject _enemyParent = null;

        private string _path;
        
        private void CreateEnemy()
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(_minInclusive_X, _maxInclusive_X),_height,UnityEngine.Random.Range(_minInclusive_Z, _maxInclusive_Z));
            GameObject obj = GameManager.Resources.Instantiate(_path, position, _enemyParent.transform);
            Debug.Log("적이 생성되었습니다. ");
            GameManager.EnemyCount++;
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
    
    private IEnumerator _enemyCoroutine = null;

    [SerializeField] private float spawningTime = 100.0f;
    
    private string path = "EnemyEX";
    
    public void Init()
    {
        enemyParent = GameObject.Find("@EnemyParent");
        if(enemyParent == null)
            enemyParent = new GameObject("@EnemyParent");

        _enemyCoroutine = EnemySpawner();
        StartCoroutine(_enemyCoroutine);
    }

    //옮길 수 있을 것 같음
    public void OnUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
            StopCoroutine(_enemyCoroutine);
        //if (Input.GetKey(KeyCode.C))
            //enemyCount--;
        //if(enemyCount == 0)
            //GameManager.UI.ShowPopupUI<UI_GameClear>();
    }

    
    
}
