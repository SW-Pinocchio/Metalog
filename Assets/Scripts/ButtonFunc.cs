using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunc : MonoBehaviour
{
    [SerializeField]
    private int LoadSceneNumber;

    public void ClickToLoadScene()
    {
        SceneManager.LoadSceneAsync(LoadSceneNumber);
    }

}
