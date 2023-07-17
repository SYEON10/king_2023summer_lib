using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

//JsonEx.cs, DataManager에서 예시 및 테스트로 사용되는 클래스
//흔한 변수명이라 나중에 겹칠까봐 M_을 붙임
#region M_User
[Serializable]
public class M_User
{
    //private 로 선언해야 할 시 [Serializable Field] 붙여야 함.
    //대부분의 상황에서는 private 로 선언하는 게 맞음.
    
    public int userId;
    public string firstName;

    public M_User(int id, string name)
    {
        userId = id;
        firstName = name;
    }

    public void PrintName()
    {
        Debug.Log("printNam" +
                  "name : " + firstName);
    }
}
[Serializable]
public class UserData : IJsonLoader<int, M_User>
{
    public List<M_User> users = new List<M_User>();
    
    public Dictionary<int, M_User> MakeDict()
    {
        Dictionary<int, M_User> dict = new Dictionary<int, M_User>();
        foreach (M_User user in users)
            dict.Add(user.userId, user);
        return dict;
    }
}

#endregion

