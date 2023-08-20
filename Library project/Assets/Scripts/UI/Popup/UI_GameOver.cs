using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_GameOver :UI_Popup
{
    enum Btns
    {
        Button_Retry,
    }
    
    enum Texts
    {
        Text_GameOver,
        Text_Retry
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<UnityEngine.UI.Button>(typeof(Btns));


        GameObject btn = GetButton((int)Btns.Button_Retry).gameObject;
        BindUIEvent(btn, (PointerEventData data) => {Retry();});
    }

    private void Retry()
    {
        GameManager.UI.ClosePopupUI();
        GameManager.PlayerAlive = true;
        GameManager.BossAlive = true;
        GameManager.Scene.LoadScene(Define.Scene.Game);
    }
}
