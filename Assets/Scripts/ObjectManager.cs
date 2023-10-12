using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private List<GameObject> objectsList = new List<GameObject>();

    public void AddObject(GameObject obj)
    {
        objectsList.Add(obj);
        if(Mathf.Abs(obj.transform.position.x) < 2f && Mathf.Abs(obj.transform.position.y) < 2f)
        {
            RemoveObject(obj);
        }
        Debug.Log(objectsList.Count);
    }

    public void RemoveObject(GameObject obj)
    {
        if (objectsList.Contains(obj))
        {
            objectsList.Remove(obj);
            Destroy(obj);
        }
    }
}