using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum ColType
{
    SceneMove,
    UIOpen,
    Exit,
};

public class ColliderEvent : MonoBehaviour
{
    [SerializeField]
    private ColType colType;

    [SerializeField]
    private int SceneNum = 0;

    [SerializeField]
    private GameObject UIObject = null;

    private void OnTriggerEnter(Collider other)
    {
        switch (colType)
        {
            case ColType.SceneMove:
                SceneManager.LoadSceneAsync(SceneNum);
                break;
            case ColType.UIOpen:
                UIObject.SetActive(true);
                break;
            case ColType.Exit:
                Application.Quit();
                break;
            default:
                break;
        }
    }



}
