using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Boss : UI_Scene
{
    private TextMeshProUGUI cooltimeCount = null;
    private PlayerAttack player = null;
    enum Texts
    {
        Text_Count
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));

        cooltimeCount = GetText((int)Texts.Text_Count);
        player = GameObject.Find("Player").GetOrAddComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        cooltimeCount.text = string.Format("{0:0.#}",player.P_LeftCoolTime);
    }
}
