using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    private IEnumerator coroutine = null;
    private int enemyCount = 1;
    [SerializeField] private float spawningTime = 100.0f;
    
    private GameObject enemyParent = null;
    private string path = "EnemyEX";
    [SerializeField] private float waitingTime = 3.0f;
    [SerializeField] private int minInclusive_X = 450;
    [SerializeField] private int maxInclusive_X = 550;
    [SerializeField] private int minInclusive_Z = 450;
    [SerializeField] private int maxInclusive_Z = 550;
    [SerializeField] private float height = 1.0f;
    private void Start()
    {
        enemyParent = GameObject.Find("@EnemyParent");
        if(enemyParent == null)
            enemyParent = new GameObject("@EnemyParent");
        coroutine = SpawnEnemy();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            StopCoroutine(coroutine);
        if (Input.GetKey(KeyCode.C))
            enemyCount--;
        if(enemyCount == 0)
            GameManager.UI.ShowPopupUI<UI_GameClear>();
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(minInclusive_X, maxInclusive_X),height,UnityEngine.Random.Range(minInclusive_Z, maxInclusive_Z));
            GameObject obj = GameManager.Resources.Instantiate(path, position, enemyParent.transform);
            enemyCount++;
            Debug.Log("적이 생성되었습니다. ");
            yield return new WaitForSeconds(waitingTime);
        }
    }
    
}
