using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private List<GameObject> objectsList = new List<GameObject>();
    private DataBaseManager DatabaseManager;
    [SerializeField]
    private GameObject Parent;

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
        DatabaseManager.LoadDataFromPHP();                
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
}