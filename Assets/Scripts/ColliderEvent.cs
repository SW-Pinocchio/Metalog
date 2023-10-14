using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [SerializeField]
    private GameObject LoadUI;

    private void OnTriggerEnter(Collider other)
    {
        switch (colType)
        {
            case ColType.SceneMove:
                SceneManager.LoadSceneAsync(SceneNum);
                break;
            case ColType.UIOpen:
                UIObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case ColType.Exit:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(SceneNum);
        }
    }





}
