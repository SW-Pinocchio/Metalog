using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunc : MonoBehaviour
{
    [SerializeField]
    private int LoadSceneNumber;

    [SerializeField]
    private GameObject target;

    public void ClickToLoadScene()
    {
        SceneManager.LoadSceneAsync(LoadSceneNumber);
    }

    public void CloseObject()
    {
        target.SetActive(false);
    }

}
