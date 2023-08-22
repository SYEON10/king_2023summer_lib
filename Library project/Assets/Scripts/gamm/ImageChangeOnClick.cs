using UnityEngine;
using UnityEngine.UI;

public class ImageChangeOnClick : MonoBehaviour
{
    public Image imageToChange; // 변경할 이미지
    public Sprite newImageSprite; // 새로운 이미지 스프라이트

    private bool hasImageChanged = false;


    void Update()
    {

        if (Input.GetMouseButtonDown(0) == true)
        {

            imageToChange.sprite = newImageSprite;
            Invoke("LoadNextScene", 1.0f);
        }

    }

    void LoadNextScene()
    {
        GameManager.Scene.LoadScene(Define.Scene.Game);
    }

}