using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostManager : MonoBehaviour
{

    [SerializeField]
    Text title_tt;
    [SerializeField]
    Text content_tt;
    [SerializeField]
    GameObject UIcanvas;

    DataBaseManager databasemanager;

    int objID;
    int postID;

    private void Awake()
    {
        databasemanager = FindObjectOfType<DataBaseManager>();
    }

    public void OpenPost(int oid, int pid)
    {
        objID = oid;
        postID = pid;
        UIcanvas.SetActive(true);
    }
    public void WritePost()
    {
        databasemanager.AddPost(PlayerPrefs.GetInt("UserID"), 1, 1, title_tt.text, content_tt.text);
    }

    public void LoadPost ()
    {

    }

    public void closeUI()
    {
        UIcanvas.SetActive(false);
    }
}
