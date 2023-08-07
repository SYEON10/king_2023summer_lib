using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTEst : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player")
        {
            GameObject _player = GameObject.Find("Player");
            GameManager.Resources.Destroy(_player);
            GameManager.UI.ShowPopupUI<UI_GameOver>();
        }
    }
}