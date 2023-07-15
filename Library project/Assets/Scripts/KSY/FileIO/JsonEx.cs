/*
  [Not Finished]
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

    public void PrintName()
    {
        Debug.Log("printName : " + userId);
    }
}
public class JsonEx : MonoBehaviour
{
    void Start()
    {
        JsonFile file = new JsonFile("Scripts/KSY/FileIO/Test");
        User user = new User();
        user = file.Read<User>();
        user.PrintName();
    }
}
