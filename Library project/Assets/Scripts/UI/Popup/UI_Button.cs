using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        PointButton,
        CloseButton,
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        
        GetButton((int)Buttons.PointButton).gameObject.BindUIEvent(OnButtonClicked);
        
        GameObject img = GetImage((int)Images.ItemIcon).gameObject;
        BindUIEvent(img, (PointerEventData data) => { img.transform.position = data.position; }, Define.UIEvent.Drag);

        GameObject btn = GetButton((int)Buttons.CloseButton).gameObject;
        BindUIEvent(btn, (PointerEventData data) => {GameManager.UI.ClosePopupUI();});
    }

    private int _score;
    public void OnButtonClicked(PointerEventData eventData)
    {
        Debug.Log("Button Clicked");
        _score++;
        GetText((int)Texts.ScoreText).text = _score.ToString();

    }
}
