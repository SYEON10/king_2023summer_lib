using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    //SoundManager soundManager1;
    //public AudioClip audio2;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 50.0f; 
    public float bulletLifetime = 1.0f;
    private int toggleView = 3;

    void Start()
    {
       // soundManager1 = new SoundManager();
        //soundManager1.Init(); // SoundManager 초기화
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(2)) //휠클릭, 1/3인칭 시점 전환
            toggleView = 4 - toggleView; //1>3 클릭때마다 변함 


        if (toggleView == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
               // soundManager1.Play(audio2, Define.Sound.Effect);
                ShootBullet();
                
            }
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null) return;

        
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 hitPoint = hitInfo.point;
            Vector3 bulletDirection = (hitPoint - bullet.transform.position).normalized;

 
            bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletSpeed, ForceMode.Impulse);

    
        }
        Destroy(bullet, bulletLifetime);
    }
}