using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainscene : MonoBehaviour
{
    public Image imageToChange; // 변경할 이미지
    public Sprite newImageSprite; // 새로운 이미지 스프라이트

    private bool hasImageChanged = false;

    public void OnButtonClick()
    {
        if (!hasImageChanged)
        {
            imageToChange.sprite = newImageSprite;
            hasImageChanged = true;

            // 1초 후에 다음 씬으로 이동
            Invoke("LoadNextScene", 1.0f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("다음씬이름"); // 다음 씬의 이름으로 변경
    }
}