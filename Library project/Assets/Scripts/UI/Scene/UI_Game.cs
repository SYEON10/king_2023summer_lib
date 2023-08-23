using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Game : UI_Scene
{
    private TextMeshProUGUI cooltimeCount = null;
    private PlayerAttack player = null;
    private TextMeshProUGUI ultimateCount = null;
    private TextMeshProUGUI time = null;

    enum Texts
    {
        Text_Time,
        Text_CoolCount,
        Text_UltimateCount
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));

        cooltimeCount = GetText((int)Texts.Text_CoolCount);
        ultimateCount = GetText((int)Texts.Text_UltimateCount);
        time = GetText((int)Texts.Text_Time);

        player = GameObject.FindGameObjectWithTag("Player").GetOrAddComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        time.text = string.Format("{0:00.#}", GameManager.Timer);
        cooltimeCount.text = string.Format("{0:0.#}",player.P_LeftCoolTime);
        ultimateCount.text = player.ultimateCharges.ToString();
    }
}
