using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player")
        {
            GameManager.GameOver();
            GameManager.BossAlive = false;
        }
    }
}
