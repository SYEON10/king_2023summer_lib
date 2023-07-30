using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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

#region  M_Dialogue2

public class M_Dialogue2{
    public string _name;
    public string _content;
    public string _selection;

    public M_Dialogue2(string name, string content, string selection){
        _name = name;
        _content = content;
        _selection = selection;
    }

}

public class M_DialogueData2 : CsvLoader<M_Dialogue2>
{
    public List<M_Dialogue2> List
    {
        get => _list;
        set => _list = value; 
    }
    public override M_Dialogue2 MakeInstance(string[] parse)
    {
        return new M_Dialogue2(parse[0], parse[1], parse[2]);
    }
}

#endregion

