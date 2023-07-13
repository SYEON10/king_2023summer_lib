using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    protected int _mp;
    protected float _time;
    protected Collider _scale;
    protected Vector3 _location;
    
    //다른 생성자를 생성할 가능성이 높기 때문에 Default 생성자를 미리 만들어둠
    public Skill(){}
    public virtual void ActivateSkill(){ Debug.Log("등록되지 않은 스킬을 실행시켰습니다.");}
}

class AttackSkill : Skill
{
    protected int _atk;
    
    protected AttackSkill(){}
}

class Bochgun : AttackSkill
{    
    public Bochgun(){}
    public override void ActivateSkill()
    {
        Debug.Log("이어스트 보흐건이 실행되었습니다.");
    }
}

//단일 인스턴스를 보장하고 싶지만 전역으로 사용하고 싶지는 않음.
//방법이 있을까?
public class KSY_Skill
{
    public Dictionary<KeyCode, Skill> Skills;
    private readonly List<KeyCode> _keyList;
    public List<KeyCode> KeyList {get{return _keyList;}}

    public KSY_Skill()
    {
        Skills = new Dictionary<KeyCode, Skill>();
        _keyList = new List<KeyCode>{KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };
        foreach(KeyCode key in _keyList)
        {
            Skills.Add(key, new Skill());
        }
        //Ex. 보흐건 실행
        //추후 인터페이스가 생기면 거기서 스킬 등록을 하는 기능을 만들겠음
        Skills[_keyList[0]] = new Bochgun();
    }

}
