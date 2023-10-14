using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunc : MonoBehaviour
{
    [SerializeField]
    private int LoadSceneNumber;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    Text ID;

    public void ClickToLoadScene()
    {
        SceneManager.LoadSceneAsync(LoadSceneNumber);
    }

    public void CloseObject()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        target.SetActive(false);
    }

    public void ClickToInput()
    {
        try
        {
            int number = Convert.ToInt32(ID.text);
            PlayerPrefs.SetInt("UserID", number);
            Debug.Log("ID = " + PlayerPrefs.GetInt("UserID"));
            ClickToLoadScene();
            // ���ڿ��� ������ ��ȯ�� ��(number)�� ����� �� �ֽ��ϴ�.
        }
        catch (FormatException e)
        {
            // ��ȯ�� �� ���� ���ڿ��� �Էµ� ��� ���⿡�� ���� ó���� �� �� �ֽ��ϴ�.
            Debug.LogError("Invalid number format: " + e.Message);
        }
        
    }

}
