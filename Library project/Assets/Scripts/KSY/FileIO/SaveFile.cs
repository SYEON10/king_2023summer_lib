#region About Json File
/*
Json 파일로 객체를 저장/불러오기를 할 수 있습니다.
Json 파일은 가시적으로 보기 편하고, XML보다 가볍다는 장점이 있습니다.

[] -> 배열, 리스트
{} -> 구조체, 클래스

본 JsonFile은 Json 파일을 읽고 쓰기 위해 작성되었다.
Json으로 동적 데이터를 저장하기 위해 작성되었으며 정적 데이터를 읽어오는 작업은 DataManager.cs 스크립트에 작성한다.
만약 서버가 필요하지 않은 게임을 만든다면 게임 초기화 용도로 사용될 것 같으므로 메모리에 올렸다면 using을 통해 사용이 끝나는 즉시 닫히도록 하는 게 좋음.

주의사항
받아오는 리스트의 변수명과 json 파일 안의 리스트 변수명이 일치해야 한다. 필드명 또한 마찬가지.
private는 저장되지 않으며, 저장해야 할 경우에는 [Serializable] 을 위에 붙여야 한다.

 */
#endregion

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
Write : 런타임 중 불가능
StreamReader로 접근 가능

Purpose : 게임 도중 읽어올 데이터 저장. (Ex. 시작 포인트, 플레이어의 Max 체력, 캐릭터의 대사, 등)
게임의 업데이트 시 변경될 수 있음.
에셋번들 저장 시에 사용

3. Application.persistentDataPath
Read : 런타임 중 가능
Write : 런타임 중 가능

Purpose : 게임의 현재 상태를 저장. (Ex. 현재 위치, 현재 플레이어의 체력, 등 플레이어 Save data)
게임과 분리된 사용자 폴더에 저장됨.
게임의 업데이트(버전)와 무관한 데이터 저장.

*/
#endregion

#region How to use IniFile and JsonFile
/*
사용방법
IniEx.cs 참고

아래 클래스는 경로를 매개변수로 하여 ini 파일을 읽고 쓸 수 있도록 하는 인스턴스를 생성합니다.
인스턴스를 통해 ini 파일을 읽고 쓸 수 잇습니다.

언제 사용하느냐
인스턴스 저장 -> JsonFile
설정 등 단순 정보 저장 -> IniFile

@주의점@
파일 경로를 작성할 때 확장자를 작성해야 함
Json 파일 속 변수명 반드시 클래스 변수명과 일치해야 함

파일 열고 다 쓰는 데까지
using(파일열기){파일사용}
으로 작성하면 키워드가 알아서 파일을 닫아줍니다.
*/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

//다형성 이용도 어려울 것 같은데 이게 의미가 있을까?
//굳이 ini랑 json을 같은 컬렉션에 넣어서 저장할 이유는 없지요...
//이게 더 효율적이긴 할까...
public class SaveFile
{
    protected readonly string _basePath = Application.persistentDataPath + "/";
    protected string _filePath;
    protected SaveFile(string filePath, string basePath = null)
    {
        if(basePath != null)
            _basePath = basePath;
        
        _filePath = _basePath + filePath;
        
        if(!File.Exists(_filePath))
        {
            File.Create(_filePath);
            Debug.Log($"파일이 생성되었습니다. name : {_filePath}");
        }
        else
        {
            Debug.Log($"파일을 정상적으로 찾았습니다. name : {_filePath}");
        }
    }

}
public class JsonFile : SaveFile
{
    //에러로그 출력에 대한 더 좋은 방법이 있을 것 같은데 방법이 생각이 안 남...
    //어차피 코드 에러 확인을 위해 작성한 거라서 실제 실행 시에 에러가 날 가능성은 없기는 한데... (파일에서 Test.json 같은 문자열을 읽어오는 경우가 아니라면)
    //Virtual 함수 사용하려 했는데 부모 생성자에서 실행할 수 없었음... (실행순서...)
    public JsonFile(string filePath, string basePath = null) : base(filePath, basePath)
    {
        if (Path.GetExtension(filePath) != ".json")
            Debug.Log("@ERROR@ 해당 파일의 확장자는 json가 아님에도 json로 열기를 시도했습니다.");
    }

    public void Write<T>(T instance)
    {
        string content = JsonConvert.SerializeObject(instance, Formatting.Indented);
        File.WriteAllText(_filePath, content);
    }

    public T Read<T>(){
        string content = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<T>(content);
    }
    
}
public class IniFile : SaveFile
{
    private readonly int _strBuilderSize;

    #region kernel32.dll import

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string defaultVal, StringBuilder returnVal, int size, string filePath);

    #endregion

    public IniFile(string filePath, int strBuilderSize = 255, string basePath = null) : base(filePath, basePath)
    {
        _strBuilderSize = strBuilderSize;
        
        if(Path.GetExtension(filePath) != ".ini")
            Debug.Log("@ERROR@ 해당 파일의 확장자는 ini가 아님에도 ini로 열기를 시도했습니다.");
            
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

