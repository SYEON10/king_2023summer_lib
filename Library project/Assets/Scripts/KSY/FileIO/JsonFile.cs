/*
Programmer : KangSYEON
      Date : 7/7/2023
   Purpose : Read Json File
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#region About Json File
/*
Json 파일로 객체를 저장/불러오기를 할 수 있습니다.
Json 파일은 가시적으로 보기 편하고, XML보다 가볍다는 장점이 있습니다.

본 JsonFile은 Json 파일을 읽어오기 위해 작성되었다.
클래스의 형태를 가진 정적 데이터를 읽어오기 위한 목적으로 만들어졌기 때문에 Write는 불가능하다.
동적 데이터의 저장은 .ini 파일을 이용하면 된다.
 */
#endregion

public class JsonFile
{
    private readonly string _basePath = Application.dataPath + "/";
    private readonly string _filePath;

    public JsonFile(string filePath = null, string basePath = null){
        if(basePath != null)
            _basePath = basePath;
        
        _filePath = _basePath + (filePath ?? "Test") + ".json";

        if(!File.Exists(_filePath))
            Debug.Log(filePath+"이름의 json 파일이 존재하지 않습니다.");

    }

    public T Read<T>(){
        string content = File.ReadAllText(_filePath);
        return JsonUtility.FromJson<T>(content);
    }
    
}
