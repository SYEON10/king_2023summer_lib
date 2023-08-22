using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainscene : MonoBehaviour
{
    public Image imageToChange; // ������ �̹���
    public Sprite newImageSprite; // ���ο� �̹��� ��������Ʈ

    private bool hasImageChanged = false;

    public void OnButtonClick()
    {
        if (!hasImageChanged)
        {
            imageToChange.sprite = newImageSprite;
            hasImageChanged = true;

            // 1�� �Ŀ� ���� ������ �̵�
            Invoke("LoadNextScene", 1.0f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("�������̸�"); // ���� ���� �̸����� ����
    }
}