/*
Programmer : KangSYEON
      Date : 7/6/2023
   Purpose : I/O .ini File.
*/

#region About INI File
/*
INI 파일은 config 파일의 표준입니다.
섹션, 키, 값으로 이루어져 있습니다.

[섹션]
키 = 값
의 구성으로 데이터를 저장할 수 있습니다.
*/
#endregion

#region About Folder to Save Files
/*

유니티에서 파일을 저장할 수 있는 폴더는 3가지가 있습니다.

1. Application.dataPath(Resources)
2. Application.streamingAssetsPath
3. Application.persistentDataPath

1. Application.dataPath(Resources)
Read : 런타임 중 가능
Write : 런타임 중 불가능
Resources.Load로 접근 가능

Purpose : Asset 저장.
빌드에 포함됨.

2. Application.streamingAssetsPath
Read : 런타임 중 가능
Write : 가능
StreamReader로 접근 가능

Purpose : 게임 도중 IO로 읽어올 데이터 저장. (Ex. 시작 포인트, 플레이어의 Max 체력, 캐릭터의 대사, 등)
게임의 업데이트 시 변경될 수 있음.

3. Application.persistentDataPath
Read : 런타임 중 가능
Write : 가능

Purpose : 게임의 현재 상태를 저장. (Ex. 현재 위치, 현재 플레이어의 체력, 등 플레이어 Save data)
게임과 분리된 사용자 폴더에 저장됨.
게임의 업데이트(버전)와 무관한 데이터 저장.

*/
#endregion

#region How to use
/*
사용방법
IniEx.cs 참고

아래 클래스는 경로를 매개변수로 하여 ini 파일을 읽고 쓸 수 있도록 하는 인스턴스를 생성합니다.
인스턴스를 통해 ini 파일을 읽고 쓸 수 잇습니다.

config의 용도로 사용할 예정이기 때문에 Application.persistentDataPath를 기본 Path로 사용합니다.
하지만 _basePath를 배열로 생성해 매개변수를 추가로 받는 방식으로 확장할 수 있습니다.

추후 가비지콜렉터(using{})을 강제하는 방식으로 발전시킬 수 있을 것 같습니다.
*/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{
    private readonly string _basePath = Application.persistentDataPath + "/";
    private readonly int _strBuilderSize;
    private readonly string _filePath;
    
    #region kernel32.dll import

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string defaultVal, StringBuilder returnVal, int size, string filePath);

    #endregion

    public IniFile(string filePath = null, int strBuilderSize = 255, string basePath = null)
    {
        if (basePath != null)
            _basePath = basePath;
        
        _filePath = _basePath + (filePath ?? "config" + ".ini");
        _strBuilderSize = strBuilderSize;

        if(!File.Exists(_filePath))
        {
            File.Create(_filePath);
            
            if(File.Exists(_filePath))
                Debug.Log($"INI 파일이 생성되었습니다. name : {_filePath}");
        }
        else
        {
            Debug.Log($"INI 파일을 정상적으로 찾았습니다. name : {_filePath}");
        }
    }

    public string Read(string section, string key, string defaultVal = ""){
        var temp = new StringBuilder(_strBuilderSize);
        GetPrivateProfileString(section, key, defaultVal, temp, _strBuilderSize, _filePath);

        string value = temp.ToString();
        
        Debug.Log($"ini File을 읽어왔습니다. data : {value}");

        if (value == defaultVal)
            Debug.Log($"읽어온 ini File 의 데이터가 defaultVal과 동일합니다. ");
        
        return value;
    }

    public void Write(string section, string key, string val)
    {
        WritePrivateProfileString(section, key, val, _filePath);
    }

    public void ViewFile()
    {
        Debug.Log(File.ReadAllText(_filePath));
    }

    public void DeleteSection(string section)
    {
        Write(section, null, null);
    }

    public void DeleteKey(string section, string key)
    {
        Write(section, key, null);
    }

    public bool IsKeyExist(string section, string key)
    {
        Debug.Log("Call Read - IsKeyExist / ignore below 2 logs");
        return Read(section, key).Length > 0;
    }
}
