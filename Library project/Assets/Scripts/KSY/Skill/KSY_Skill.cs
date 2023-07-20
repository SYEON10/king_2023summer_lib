using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    protected int _mp;
    protected float _runningTime;
    protected Collider _scale;
    protected Vector3 _location;
    
    //다른 생성자를 생성할 가능성이 높기 때문에 Default 생성자를 미리 만들어둠
    public Skill(){}
    public virtual void ActivateSkill(){ Debug.Log("등록되지 않은 스킬을 실행시켰습니다.");}
    
    /*
    protected virtual IEnumerator SkillDelay(float _runningTime){
        yield return new WaitForSeconds(_runningTime);
    }
    */
}

class AttackSkill : Skill
{
    protected int _atk;
    
    protected AttackSkill(){}
}

class Bochgun : AttackSkill
{
    private float _aimingTime = 5.0f; //각도 조절하는 시간
    bool _isFired = false;
    GameObject _spear = null;
    
    
    public Bochgun(){}
    public override void ActivateSkill()
    {
        _spear = GameManager.Resources.Instantiate("Weapon/Spear");
        _spear.transform.position = Player.player.transform.position + new Vector3(2.0f, 0, 0); //나중에 변수로 빼겠습니다...
        GameManager.Input.KeyAction -= Aiming;
        GameManager.Input.KeyAction += Aiming;

        CoroutineHelper.RunCoroutine(AimingDelay());

        Debug.Log("이어스트 보흐건이 실행되었습니다.");
    }

    void Fire(){
        Debug.Log("이어스트 보흐건이 발사되었습니다.");
        GameManager.Input.KeyAction -= Aiming;
    }
    
    void Aiming()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(_spear.transform.rotation.z <= 90f) //실행 안됨. 원인 불명.
                _spear.transform.Rotate(new Vector3(0, 0, 1.0f));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if(_spear.transform.rotation.z >= 0)
                _spear.transform.Rotate(new Vector3(0, 0, -1.0f));
        }
    }

    IEnumerator AimingDelay(){
        yield return new WaitForSeconds(_aimingTime);
        if(_isFired == false)
            Fire();
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
