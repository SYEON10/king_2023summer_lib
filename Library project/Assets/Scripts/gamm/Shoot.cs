using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
   
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 50.0f; 
    public float bulletLifetime = 1.0f; 




    void Start()
    {

    }

    void Update()
    {


        
        if (Input.GetMouseButtonDown(0)) 
        {
            ShootBullet();
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