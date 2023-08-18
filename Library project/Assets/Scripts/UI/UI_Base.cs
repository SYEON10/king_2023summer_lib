using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else 
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            
            if(objects[i] == null)
                Debug.Log($"Failed to Bind!{names[i]}");
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            Debug.Log("@ERROR@UI를 가져오는 것을 실패했습니다.");
            return null;
        }

        return objects[index] as T;
    }

    protected TextMeshProUGUI GetText(int index)
    {
        return Get<TextMeshProUGUI>(index);
    }
    
    protected UnityEngine.UI.Button GetButton(int index)
    {
        return Get<UnityEngine.UI.Button>(index);
    }
    
    protected Image GetImage(int index)
    {
        return Get<Image>(index);
    }

    protected GameObject GetObject(int index)
    {
        return Get<GameObject>(index);
    }

    public static void BindUIEvent(GameObject obj, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(obj);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}
