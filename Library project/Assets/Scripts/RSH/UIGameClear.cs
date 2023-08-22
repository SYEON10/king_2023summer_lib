using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UIGameClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BackToMain_Btn()
    {
        GameManager.Scene.LoadScene(Scene.Main);
    }

    public void ReStartGame_Btn()
    {
        GameManager.Scene.LoadScene(Scene.Game);
    }
}
