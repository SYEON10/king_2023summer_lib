using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSY_PlayerController : MonoBehaviour
{
    private KSY_Skill _skill;
    public void Start()
    {
        _skill = new KSY_Skill();
        GameManager.Input.KeyAction -= SkilUpdate;
        GameManager.Input.KeyAction += SkilUpdate;
    }

    // Update is called once per frame
    public void SkilUpdate(){

        foreach (KeyCode key in _skill.KeyList)
        {
            //지금은 GetKeyDown으로 했지만
            //연타, 혹은 이어스트 보흐건처럼 누르는 시간을 체크해야 하는 게 있을 수 있으므로 추후 이 부분을 발전시키겠음
            if(Input.GetKeyDown((key)))
                _skill.Skills[key].ActivateSkill();
        }
    }
}
