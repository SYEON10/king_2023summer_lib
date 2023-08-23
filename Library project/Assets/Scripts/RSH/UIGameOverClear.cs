using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UIGameOverClear : MonoBehaviour
{
    [SerializeField] private GameObject _EasterEgg;
    public void BackToMain_Btn()
    {
        GameManager.Scene.LoadScene(Scene.Main);
    }

    public void ReStartGame_Btn()
    {
        GameManager.Retry();
    }

    public void EasterEgg()
    {
        _EasterEgg.SetActive(true);
    }

    public void CloseEasterEgg()
    {
        _EasterEgg.SetActive(false);
    }
}
