using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Boss : UI_Scene
{
    private TextMeshProUGUI cooltimeCount = null;
    private PlayerAttack player = null;
    private TextMeshProUGUI ultimateCount = null;

    enum Texts
    {
        Text_CoolCount,
        Text_UltimateCount
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

        cooltimeCount = GetText((int)Texts.Text_CoolCount);
        ultimateCount = GetText((int)Texts.Text_UltimateCount);

        player = GameObject.FindGameObjectWithTag("Player").GetOrAddComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        cooltimeCount.text = string.Format("{0:0.#}",player.P_LeftCoolTime);
        ultimateCount.text = player.ultimateCharges.ToString();
    }
}
