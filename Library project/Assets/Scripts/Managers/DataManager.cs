using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public interface IJsonLoader<TKey,TValue>
{
    Dictionary<TKey, TValue> MakeDict();
}

/*
파일 내에서
[Start]
[End]
를 기준으로 Dictionary를 생성
*/
public abstract class CsvLoader<TValue>
{
    protected List<TValue> _list = new List<TValue>();
    
    //데이터마다 다르게 구현해야 하는 함수를 abstract 로 강제
    public abstract TValue MakeInstance(string[] content);
    public List<TValue> MakeList(StringReader reader)
    {
        while (true)
        {
            string line = reader.ReadLine();
            if (line == null)
                break;
            string[] parse = line.Split(',');
            _list.Add(MakeInstance(parse));
        }

        _list.RemoveAt(_list.Count - 1);
        return _list;
    }

    public Dictionary<string, List<TValue>> MakeDict(StringReader reader)
    {
        Dictionary<string, List<TValue>> dict = new Dictionary<string, List<TValue>>();
        
        string line = reader.ReadToEnd();
        
        Debug.Log(line);
        
        if (line == null)
        {
            Debug.Log("@ERROR@빈 csv 파일을 읽으려고 시도했습니다.");
            return null;
        }
        
        string[] parse = line.Split("[End]");
        
        Debug.Log(parse[0]);
        Debug.Log(parse[1]);
        
        for(int i = 0; i < parse.Length; i++)
        {
            StringReader readLine = new StringReader(parse[i]);
            if(i != 0)
                readLine.ReadLine(); //[End] 줄을 날림
            string name = readLine.ReadLine();
            string[] parseLine = name.Split(',');
            Debug.Log(parseLine[1]);
            dict.Add(parseLine[1],MakeList(readLine));
        }
        
        return dict;
    }
    
}

public class DataManager
{
    //정적 데이터는 전역으로 들고 있는 게 좋음
    //Dialogue 같은 애들은 그때그때 불러오는 게 메모리 회수에 더 좋을 것 같기는 해서... 좋은 방법 있으면 알려주세요
    public Dictionary<int, M_User> UserDict { get; private set; }
    public List<M_Dialogue> DiaList { get; private set; }
    public Dictionary<string, List<M_Dialogue2>> DiaDict {get; private set;}

    public void Init()
    {
        UserDict = LoadJson<UserData, int, M_User>("Test").MakeDict();
        UserDict[1].PrintName();

        DiaList = LoadCsv<M_DialogueData, M_Dialogue>("Dialogue");
        DiaList[1].printName();
        
        DiaDict = LoadCsv2<M_DialogueData2, M_Dialogue2>("Dialogue2");
        Debug.Log(DiaDict["EX2"][0]._name);
    }

    //인터페이스에 있는 함수를 구현한 클래스만 Load 가능하게 강제
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : IJsonLoader<Key, Value>
    {
        TextAsset content = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(content.text);
    }

    List<Value> LoadCsv<Loader, Value>(string path) where Loader : CsvLoader<Value>, new()
    {
        return new Loader().MakeList(CsvReader(path));
    }

    Dictionary<string, List<Value>> LoadCsv2<Loader, Value>(string path) where Loader : CsvLoader<Value>, new()
    {
        return new Loader().MakeDict(CsvReader(path));
    }

    StringReader CsvReader(string path)
    {
        TextAsset content = Resources.Load<TextAsset>($"Data/{path}");
        Debug.Log(content.text);
        return new StringReader(content.text);
    }
    
}