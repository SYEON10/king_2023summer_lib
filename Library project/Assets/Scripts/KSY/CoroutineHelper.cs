using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private IEnumerator _enumerator = null;
    private void Coroutine(IEnumerator enumerator)
    {
        _enumerator = enumerator;
        StartCoroutine(_enumerator);
    }

    public void Stop()
    {
        StopCoroutine(_enumerator.ToString());
        Destroy(gameObject);
    }

    public static CoroutineHelper RunCoroutine(IEnumerator enumerator)
    {
        GameObject obj = new GameObject("@CoroutineHelper");
        CoroutineHelper _helper = obj.AddComponent<CoroutineHelper>();
        if(_helper)
            _helper.Coroutine(enumerator);
        return _helper;
    }
}
