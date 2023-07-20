using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : UI_Scene
{
    enum GameObjects
    {
        GridPanel,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            GameManager.Resources.Destroy(child.gameObject);

        for (int i = 0; i < 8; i++)
        {
            UI_Inventory_item item = GameManager.UI.MakeSubItem<UI_Inventory_item>(gridPanel.transform, "UI_Inventory_item");
            item.SetInfo($"{i}th Item");
        }
        
        //Temp
        GameManager.UI.ShowPopupUI<UI_Button>();
    }
    
}
