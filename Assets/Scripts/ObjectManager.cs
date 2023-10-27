using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    private List<GameObject> objectsList = new List<GameObject>();
    private DataBaseManager DatabaseManager;
    [SerializeField]
    private GameObject Parent;

    [SerializeField]
    private GameObject Input;
    [SerializeField]
    private InputField text;

    private void Awake()
    {
        DatabaseManager = FindObjectOfType<DataBaseManager>();
        LoadLegacyFurniture();
    }
    public void AddObject(GameObject obj, int objType)
    {
        objectsList.Add(obj);
        obj.GetComponent<ObjectPost>().SetID(objectsList.IndexOf(obj));
        DatabaseManager.AddFurniture(obj, PlayerPrefs.GetInt("UserID"), objectsList.IndexOf(obj), objType);
    }

    public void RemoveObject(GameObject obj)
    {
        if (objectsList.Contains(obj))
        {
            objectsList.Remove(obj);
            Destroy(obj);
        }
    }

    public void LoadLegacyFurniture()
    {
        DatabaseManager.LoadDataFromPHP(PlayerPrefs.GetInt("UserID"));
    }

    public void ClearFurniture()
    {
        DatabaseManager.DeleteDataForUserID(PlayerPrefs.GetInt("UserID"));
        for (int i = Parent.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = Parent.transform.GetChild(i);
            Destroy(child.gameObject);
        }
        
    }

    public void LoadAnother()
    {
        if(Input.activeSelf)
        {
            Input.SetActive(false);
        }
        else
        {
            Input.SetActive(true);
        }
        
    }

    public void ClicktoVisit()
    {
        int id;
        if(int.TryParse(text.text, out id))
        {
            for (int i = Parent.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = Parent.transform.GetChild(i);
                Destroy(child.gameObject);
            }
            DatabaseManager.LoadDataFromPHP(id);
        }
        
    }
}