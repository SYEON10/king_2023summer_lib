using UnityEngine;
using UnityEngine.UI;

public class ImageChangeOnClick : MonoBehaviour
{
    public Image imageToChange; // ������ �̹���
    public Sprite newImageSprite; // ���ο� �̹��� ��������Ʈ

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