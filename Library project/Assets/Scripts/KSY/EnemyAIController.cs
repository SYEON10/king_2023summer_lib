using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    private GameObject enemyParent = null;
    private string path = "EnemyEX";
    [SerializeField] private float waitingTime = 3.0f;
    [SerializeField] private int minInclusive = 450;
    [SerializeField] private int maxInclusive = 550;
    [SerializeField] private float height = 1.0f;
    private void Start()
    {
        enemyParent = new GameObject("@EnemyParent");
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(minInclusive, maxInclusive),height,UnityEngine.Random.Range(minInclusive, maxInclusive));
            GameObject obj = GameManager.Resources.Instantiate(path, position, enemyParent.transform);
            Debug.Log("적이 생성되었습니다. ");
            yield return new WaitForSeconds(waitingTime);
        }
    }

}
