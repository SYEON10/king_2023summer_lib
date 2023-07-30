using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory_item : UI_Base
{
    private string _name = "";
    enum Images
    {
        itemIcon,
    }

    enum Texts
    {
        itemNameText,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        Get<TextMeshProUGUI>((int)Texts.itemNameText).text = _name;
        Get<Image>((int)Images.itemIcon).gameObject.BindUIEvent((data) => { Debug.Log($"아이템 클릭 {_name}"); });

    }

    public void SetInfo(string itemName)
    {
        _name = itemName;
    }
}
