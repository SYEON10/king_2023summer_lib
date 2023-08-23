using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UIGameClear : MonoBehaviour
{
    public void BackToMain_Btn()
    {
        GameManager.Scene.LoadScene(Scene.Main);
    }

    public void ReStartGame_Btn()
    {
        GameManager.Retry();
    }
}
