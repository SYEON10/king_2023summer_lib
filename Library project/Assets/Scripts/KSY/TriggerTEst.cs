using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTEst : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Player")
            Debug.Log("GameOver");
    }
}
