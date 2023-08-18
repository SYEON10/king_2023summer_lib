using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10.0f;
    private float _existTime = 7.0f;

    void Update()
    {
        transform.position += transform.forward * _bulletSpeed * Time.deltaTime;
        StartCoroutine(DestroySelf());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        // TODO
        // 게임오버처리
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(_existTime);
        Destroy(gameObject);
    }
}
