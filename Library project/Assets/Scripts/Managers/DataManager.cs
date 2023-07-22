using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public interface IJsonLoader<TKey,TValue>
{
    Dictionary<TKey, TValue> MakeDict();
}

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
    
}

public class DataManager
{
    //정적 데이터는 전역으로 들고 있는 게 좋음
    //Dialogue 같은 애들은 그때그때 불러오는 게 메모리 회수에 더 좋을 것 같기는 해서... 좋은 방법 있으면 알려주세요
    public Dictionary<int, M_User> UserDict { get; private set; }
    public List<M_Dialogue> DiaList { get; private set; }

    public void Init()
    {
        UserDict = LoadJson<UserData, int, M_User>("Test").MakeDict();
        UserDict[1].PrintName();

        DiaList = LoadCsv<M_DialogueData, M_Dialogue>("Dialogue");
        DiaList[1].printName();
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

    StringReader CsvReader(string path)
    {
        TextAsset content = Resources.Load<TextAsset>($"Data/{path}");
        Debug.Log(content.text);
        return new StringReader(content.text);
    }
    
}