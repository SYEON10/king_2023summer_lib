using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    protected virtual void Init()
    {
        Object eventSystem = GameObject.FindObjectOfType(typeof(EventSystem));//GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            GameManager.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }
}
