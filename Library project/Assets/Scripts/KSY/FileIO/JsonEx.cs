using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonEx : MonoBehaviour
{
    //작성은 안 했지만 JsonFile을 실제로 열 거라면 Dictionary 혹은 List로 묶어서 관리하는 게 좋을 것.
    //다양한 저장 파일을 만들 때 (Ex. 쯔꾸르 게임에서 저장하듯) Dictionary 로 사용하면 유용할 것. 
    //위 경우 저장명을 키로 하여 파일 객체를 저장하는 방향으로 구현하면 될 듯.
    void Start()
    {
        JsonFile file = new JsonFile("Test.json");
        
        UserData userData = new UserData();
        
        userData.users.Add(new M_User(11, "John"));
        userData.users.Add(new M_User(12, "David"));
        
        file.Write(userData);
        
        userData = file.Read<UserData>();
        
        userData.users[1].PrintName();
        
        userData.users[1].firstName = "Changed!";
        file.Write(userData);
        
        userData = file.Read<UserData>();
        
        userData.users[1].PrintName();

        Dictionary<int, M_User> dict = userData.MakeDict();
        Debug.Log(dict[11].firstName);

    }
}
