using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static DataBaseManager;

public class PostManager : MonoBehaviour
{
    [SerializeField]
    GameObject UIcanvas;

    DataBaseManager databasemanager;

    public static List<PostData> pDataList;
    [System.Serializable]
    public class PostData
    {
        public int user_ID;
        public int object_ID;
        public string title;
        public string content;
    }

    int objID;

    [SerializeField]
    GameObject titlebox;
    [SerializeField]
    GameObject contentbox;
    [SerializeField]
    Text title_I;
    [SerializeField]
    Text content_I;
    [SerializeField]
    GameObject title_O;
    [SerializeField]
    GameObject content_O;
    

    private void Awake()
    {
        databasemanager = FindObjectOfType<DataBaseManager>();
    }

    public void OpenPost(int oid)
    {
        objID = oid;
        UIcanvas.SetActive(true);
        ChangeIO(false);
        LoadPost(PlayerPrefs.GetInt("UserID"), oid);        
    }
    public void WritePost()
    {
        databasemanager.AddPost(PlayerPrefs.GetInt("UserID"), objID, title_I.text, content_I.text);
        ChangeIO(true);
    }

    public void LoadPost (int id, int oid)
    {
        
        databasemanager.LoadPost(id, oid);
    }

    public void NewPost()
    {
        ChangeIO(true);
    }

    public void closeUI()
    {
        UIcanvas.SetActive(false);        
    }

    public void ChangeIO(bool t)
    {
        titlebox.SetActive(t);
        contentbox.SetActive(t);
        title_O.SetActive(!t);
        content_O.SetActive(!t);
    }

    public void DeletePost()
    {
        databasemanager.DeletePost(PlayerPrefs.GetInt("UserID"), objID);
    }
}
