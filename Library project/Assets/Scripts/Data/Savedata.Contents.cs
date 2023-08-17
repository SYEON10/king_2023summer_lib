using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DataManager.cs에서 예시 및 테스트로 사용되는 클래스
//흔한 변수명이라 나중에 겹칠까봐 M_을 붙임
#region  M_Dialogue

public class M_Dialogue{
    string _name;
    string _content;
    int _love;

    public M_Dialogue(string name, string content, int love){
        _name = name;
        _content = content;
        _love = love;
    }

    public void printName()
    {
        Debug.Log("CsvName : " + _name);
    }

}

public class M_DialogueData : CsvLoader<M_Dialogue>
{
    public List<M_Dialogue> List
    {
        get => _list;
        set => _list = value; 
    }
    public override M_Dialogue MakeInstance(string[] parse)
    {
        return new M_Dialogue(parse[0], parse[1], int.Parse(parse[2]));
    }
}

#endregion
