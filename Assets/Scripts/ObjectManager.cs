using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private List<GameObject> objectsList = new List<GameObject>();

    public void AddObject(GameObject obj)
    {
        objectsList.Add(obj);
        if(objectsList.Count > 1)
        {
            Debug.Log(objectsList[objectsList.Count - 2].gameObject.transform.position);
        }
        
        // 오브젝트 추가 후 원하는 작업 수행
    }

    // 다른 관리 기능들을 추가할 수 있습니다.
}