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
            // 문자열을 정수로 변환한 값(number)을 사용할 수 있습니다.
        }
        catch (FormatException e)
        {
            // 변환할 수 없는 문자열이 입력된 경우 여기에서 예외 처리를 할 수 있습니다.
            Debug.LogError("Invalid number format: " + e.Message);
        }
        
    }

}
