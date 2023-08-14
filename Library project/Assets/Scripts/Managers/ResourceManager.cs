using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = GameManager.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return GameManager.Pool.Pop(original, parent).gameObject;
        
        GameObject obj = Object.Instantiate(original, parent);
        obj.name = original.name;

        return obj;
    }

    public GameObject Instantiate(string path, Vector3 position, Transform parent = null)
    {
        GameObject obj = Instantiate(path, parent);
        obj.transform.position = position;
        return obj;
    }

    public void Destroy(GameObject go, float time = 0.0f)
    {
        if (go == null)
            return;
        
        //풀링이 필요하면 -> 풀링 매니저한테 던져주기
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            GameManager.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go, time);
    }
}