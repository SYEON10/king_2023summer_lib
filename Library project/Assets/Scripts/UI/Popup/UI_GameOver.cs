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
        Btn_Retry,
        
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
        Bind<Button>(typeof(Btns));

        //GameObject btn = GetButton((int)Btns.Btn_Retry).gameObject;
        //BindUIEvent(btn, (PointerEventData data) => {GameManager.UI.ClosePopupUI();});
    }
}
