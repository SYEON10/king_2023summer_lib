/*
Programmer : KangSYEON
      Date : 7/7/2023
   Purpose : I/O .ini File.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class User
{
    [SerializeField]
    private int userId;
    [SerializeField]
    private string firstName;
    [SerializeField]
    private string lastName;
    [SerializeField]
    private string phoneNumber;
    [SerializeField]
    private string emailAddress;
    [SerializeField]
    private string homepage;

    public void PrintUserId()
    {
        Debug.Log("printName : " + firstName);
    }
}
public class JsonEx : MonoBehaviour
{
    void Start()
    {
        JsonFile file = new JsonFile("Scripts/KSY/FileIO/Test");
        List<User> list = new List<User>();
        list = file.Read<List<User>>();
        Debug.Log(list.Count);
        //list[0].PrintUserId();
    }
}
